using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuizManager _quizManager = null;

    [SerializeField] private InputManager _inputManager = null;

    [SerializeField] private QuizDisplayManager _quizDisplayManager = null;

    [SerializeField] private EnemyManager _enemyManager = null;

    [SerializeField] private Player _player = null;

    [SerializeField] private HPBarManager _hpBarManager = null;

    [SerializeField] private UIConnecter _uiConnecter = null;

    [SerializeField] private TimeManager timeManager = null;
    
    [SerializeField] private AttackEffectManager _attackEffectManager = null;
    
    [SerializeField] private GameObject camera = null;

    [SerializeField] private AudioManager audioManager = null;

    // ↓新インプットシステム

    // [SerializeField] private HiraganaToRomanConverter hiraganaToRomanConverter = null;

    [SerializeField] private TextAsset dictionaryFile;

    private List<DictionaryEntry> conversionDictionary;
    private List<List<string>> romanPatterns; // 現在の入力に対応するローマ字パターン
    private int currentIndex; // 現在判定中の文字インデックス
    private string currentInput; // ユーザーの現在の入力
    private string inputedString = ""; // ユーザーの現在の入力
    private bool isComplete; // すべて入力が完了したか判定

    // ↑新インプットシステム

    public List<Vector3> playersPosition = new List<Vector3>();
    
    [SerializeField] private GameObject enemy = null;
    
    public GameObject playerPrefab = null;
    public GameObject[] dollPrefabs = null;

    // Start is called before the first frame update
    void Start()
    {
        _quizManager.ChangeQuiz();
        _uiConnecter.WhenRefreshQuiz();

        _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);

        // 敵キャラを管理するスクリプトを取得（これは全プレイヤーで１つを共有するため、直接参照するのは危ない）
        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();

        // HPバーを更新する
        _hpBarManager.InitHPBar(_player.maxHealth);

        // タイマーを初期化する
        timeManager.timer = timeManager.time;
        timeManager.status = TimeManager.Status.Playing;

        // AudioManager を参照する
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        
        //　ロビーのBGMを止めてゲームのBGMに切り替える
        audioManager.StopBGM();
        audioManager.PlayBGM(2);
        
        // プレイヤリストの取得
        int max = RoomPlayer.Players.Count;
        Debug.Log("max = " + max);

        // プレイヤーが円形に並んだ時の座標を取得する

        // 半径を取得する
        float r = Vector2.Distance(Vector2.zero, new Vector2(camera.transform.position.x, camera.transform.position.z));
        Debug.Log(string.Format("r = {0}", r));

        for (int i = 0; i < max; i++)
        {
            Debug.Log(string.Format("i = {0}", i));
            // 偏角（０～２π）を取得する
            float theta = ((float)i / max) * 2.0f * Mathf.PI - (Mathf.PI / 2.0f);

            // 座標を算出する
            float cos = Mathf.Cos(theta);
            float sin = Mathf.Sin(theta);
            Debug.Log(string.Format("cos = {0} | sin = {1}", cos, sin));
            Vector3 pos = new Vector3(cos * r, 0, sin * r);

            // プレイヤーの座標を登録する
            playersPosition.Add(pos);
        }
        
        // 人形の生成
        dollPrefabs = new GameObject[playersPosition.Count];
        
        for (int i = 0; i < playersPosition.Count; i++)
        {
            GameObject go = Instantiate(playerPrefab, playersPosition[i], Quaternion.identity);
            go.GetComponent<PlayerAttackAnimator>().TargetObject = enemy;
            dollPrefabs[i] = go;
            if (i == 0)
            {
                go.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        // ↓新インプットシステム

        LoadDictionary();

        string targetText = _quizManager.GetNowQuiz.japanese;
        romanPatterns = ConvertToRomanPatterns(targetText);
        ResetGame();

        Debug.Log("ターゲット: " + targetText);
        DebugPatterns();

        string nowInput = inputedString + currentInput;
        List<string> matches = FindMatches(nowInput);
        _quizDisplayManager.ChangeDisplayRoman(matches[0], nowInput.Length);

        // ↑新インプットシステム
    }

    //// キー入力をチェックして正しいかどうか判定するメソッド
    //bool CheckKeyInput(char inputedChar)
    //{
    //    // 現在のお題から正解文字を取得
    //    char correctChar = _quizManager.GetNowQuiz.roman[_quizManager.doneInputIndex];

    //    // 入力されたキーと正解文字を比較
    //    if (inputedChar == correctChar)
    //    {
    //        // Debug.Log("Correct key: " + inputKey);
    //        _quizManager.doneInputIndex++; // 次の文字へ
    //        _quizDisplayManager.ChangeDisplayRoman(_quizManager.GetNowQuiz, _quizManager.doneInputIndex);

    //        return true;
    //    }
    //    else
    //    {
    //        // Debug.Log("Incorrect key: " + inputKey);
    //        return false;
    //    }
    //}

    
    [SerializeField] private int _charCombo = 0;
    [SerializeField] private int _wordCombo = 0;

    // Update is called once per frame
    void Update()
    {
        if (timeManager.status != TimeManager.Status.Playing) { return; }
        
        if (_player.status != Character.Status.alive) { return; }

        if (_enemyManager.status == EnemyManager.Status.done) { return; }

        // プレイヤーからの１文字入力を受け付ける
        char inputedChar = _inputManager.GetChar();

        // 無入力の場合はリターン
        if (inputedChar == '\0') { return; }


        (bool, bool) checkInput = HandleInput(inputedChar);
        bool isCorrectChar = checkInput.Item1;
        bool isLastChar = checkInput.Item2;

        // bool isCorrectChar = CheckKeyInput(inputedChar); // 正解の入力だったか
        // bool isLastChar = (_quizManager.doneInputIndex == _quizManager.GetNowQuiz.roman.Length); // 最後の文字かどうか

        // キー入力に成功したら...
        if (isCorrectChar) 
        {
            _charCombo++;
            _uiConnecter.WhenCharComboIncreased();

            // キー入力成功の音を鳴らす
            audioManager.PlaySE(0);
            // audioManager.PlaySE(1);

            // inputedString += currentInput;

            // Debug.Log(string.Format("inputedString = {0}", inputedString));

            string nowInput = inputedString + currentInput;
            List<string> matches = FindMatches(nowInput);

            //Debug.Log("部分一致する文字列:");
            //foreach (var match in matches)
            //{
            //    Debug.Log(string.Format("match => {0}", match));
            //}

            _quizDisplayManager.ChangeDisplayRoman(matches[0], nowInput.Length);
        }

        // 最後の文字で正解ならば、クイズを更新する
        if (isCorrectChar && isLastChar)
        {
            _quizManager.ChangeQuiz();
            _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);
            
            _uiConnecter.WhenRefreshQuiz();

            // 単語を更新したら新システム側の正解も変える
            string targetText = _quizManager.GetNowQuiz.japanese;
            romanPatterns = ConvertToRomanPatterns(targetText);
            ResetGame();

            Debug.Log("ターゲット: " + targetText);
            DebugPatterns();

            string nowInput = inputedString + currentInput;
            List<string> matches = FindMatches(nowInput);
            _quizDisplayManager.ChangeDisplayRoman(matches[0], nowInput.Length);

            // 単語を更新 = 攻撃する
            int attackPower = _player.attackPower + (int)Mathf.Floor(_charCombo * 0.25f); // コンボ数を考慮した攻撃力を計算
            _enemyManager.TakeDamage(attackPower);
            _uiConnecter.WhenPlayerAttackToEnemy();
            
            _attackEffectManager.Rpc_AttackEffect(RoomPlayer.Local);

            // 単語を入力出来たら...
            _wordCombo++;

            if (_wordCombo % 5 == 0)
            {
                // ブーストを発火
                _enemyManager.Rpc_SetIsBoosting();
            }
        }

        // キー入力に失敗したら...
        if (!isCorrectChar)
        {
            // モンスターから攻撃される
            _player.Rpc_TakeDamage(_enemyManager.GetAttackPower());
            _uiConnecter.WhenEnemyAttackToPlayer();

            // プレイヤーのHPバーを更新する
            _hpBarManager.UpdatePlayerHPBar(_player.maxHealth, _player.health);

            // コンボカウンターのリセット
            _charCombo = 0;
            _wordCombo = 0;
            _uiConnecter.WhenCharComboDecreased();

            // キー入力失敗の音を鳴らす
            audioManager.PlaySE(2);

        }
    }

    // ↓新インプットシステム

    // 辞書データをロード
    private void LoadDictionary()
    {
        if (dictionaryFile == null)
        {
            Debug.LogError("辞書ファイルが指定されていません。");
            return;
        }

        string json = dictionaryFile.text;
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        conversionDictionary = new List<DictionaryEntry>(wrapper.entries);
    }

    // JSONデシリアライズ用
    [Serializable]
    private class Wrapper
    {
        public DictionaryEntry[] entries;
    }

    [Serializable]
    private class DictionaryEntry
    {
        public string Pattern;
        public List<string> TypePattern;
    }

    // ひらがなをローマ字パターンに変換
    private List<List<string>> ConvertToRomanPatterns(string input)
    {
        List<List<string>> result = new List<List<string>>();
        int i = 0;

        while (i < input.Length)
        {
            bool foundMatch = false;

            // 複合文字（2文字以上）を先にチェック
            foreach (var entry in conversionDictionary)
            {
                if (i + 1 < input.Length && input.Substring(i, 2) == entry.Pattern)
                {
                    result.Add(new List<string>(entry.TypePattern));
                    i += 2;
                    foundMatch = true;
                    break;
                }
                else if (i + 2 < input.Length && input.Substring(i, 3) == entry.Pattern)
                {
                    result.Add(new List<string>(entry.TypePattern));
                    i += 3;
                    foundMatch = true;
                    break;
                }
            }

            if (!foundMatch)
            {
                string kana = input[i].ToString();
                var entry = conversionDictionary.Find(e => e.Pattern == kana);

                if (entry != null)
                {
                    result.Add(new List<string>(entry.TypePattern));
                }
                else
                {
                    result.Add(new List<string> { kana });
                }

                i++;
            }
        }

        return result;
    }

    // ゲーム状態をリセット
    private void ResetGame()
    {
        currentIndex = 0;
        currentInput = "";
        inputedString = "";
        isComplete = false;
    }

    // 入力を処理
    public (bool, bool) HandleInput(char inputChar)
    {
        bool isCorrectChar = false;
        bool isLastChar = false;

        if (isComplete) return (isCorrectChar, isLastChar);

        currentInput += inputChar;

        if (IsCurrentInputValid())
        {
            Debug.Log($"正解: {currentInput}");

            // _quizManager.doneInputIndex++; // 次の文字へ
            // _quizDisplayManager.ChangeDisplayRoman(_quizManager.GetNowQuiz, _quizManager.doneInputIndex);

            // 現在の入力が完全一致したら次へ進む
            if (IsCurrentInputComplete())
            {

                inputedString += currentInput;

                currentIndex++;
                currentInput = "";

                if (currentIndex >= romanPatterns.Count)
                {
                    Debug.Log("全て正解しました！");
                    isComplete = true;

                    isLastChar = true;
                }
            }

            isCorrectChar = true;
        }
        else
        {
            Debug.Log($"不正解: {currentInput}");
            // 失敗した入力をなかったことにする
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
        }

        return (isCorrectChar, isLastChar);
    }

    // 現在の入力が有効なローマ字パターンに一致しているか
    private bool IsCurrentInputValid()
    {
        if (currentIndex >= romanPatterns.Count) return false;

        return romanPatterns[currentIndex].Any(pattern => pattern.StartsWith(currentInput));
    }

    // 現在の入力が完全一致したか
    private bool IsCurrentInputComplete()
    {
        if (currentIndex >= romanPatterns.Count) return false;

        return romanPatterns[currentIndex].Contains(currentInput);
    }

    // パターンをデバッグ表示
    private void DebugPatterns()
    {
        Debug.Log("変換パターン:");
        foreach (var patternList in romanPatterns)
        {
            Debug.Log(string.Join(", ", patternList));
        }
    }

    public List<string> FindMatches(string input)
    {
        // すべての候補文字列の生成
        var possibleCombinations = GenerateCombinations(romanPatterns);

        List<string> matches = new List<string>();

        foreach (var p in possibleCombinations)
        {
            // Debug.Log(string.Format("p => {0}", p));

            if (p.StartsWith(input))
            {
                matches.Add(p);
            }
        }

        return matches;
    }

    // 候補リストからすべての組み合わせを生成
    private List<string> GenerateCombinations(List<List<string>> lists)
    {
        return lists.Aggregate(
            new List<string> { "" },
            (acc, list) => acc.SelectMany(prefix => list.Select(item => prefix + item)).ToList()
        );
    }

    // ↑新インプットシステム

    // シーン遷移への架け橋
    public void TryChangeScene()
    {
        LevelManager.LoadMenu();
    }
}
