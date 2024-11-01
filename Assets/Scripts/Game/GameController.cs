using System.Collections;
using System.Collections.Generic;
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
    }

    // キー入力をチェックして正しいかどうか判定するメソッド
    bool CheckKeyInput(char inputedChar)
    {
        // 現在のお題から正解文字を取得
        char correctChar = _quizManager.GetNowQuiz.roman[_quizManager.doneInputIndex];

        // 入力されたキーと正解文字を比較
        if (inputedChar == correctChar)
        {
            // Debug.Log("Correct key: " + inputKey);
            _quizManager.doneInputIndex++; // 次の文字へ
            _quizDisplayManager.ChangeDisplayRoman(_quizManager.GetNowQuiz, _quizManager.doneInputIndex);

            return true;
        }
        else
        {
            // Debug.Log("Incorrect key: " + inputKey);
            return false;
        }
    }

    
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
        
        bool isCorrectChar = CheckKeyInput(inputedChar); // 正解の入力だったか
        bool isLastChar = (_quizManager.doneInputIndex == _quizManager.GetNowQuiz.roman.Length); // 最後の文字かどうか

        // キー入力に成功したら...
        if (isCorrectChar) 
        {
            _charCombo++;
            _uiConnecter.WhenCharComboIncreased();

            // キー入力成功の音を鳴らす
            audioManager.PlaySE(0);
            // audioManager.PlaySE(1);
        }

        // 最後の文字で正解ならば、クイズを更新する
        if (isCorrectChar && isLastChar)
        {
            _quizManager.ChangeQuiz();
            _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);
            _uiConnecter.WhenRefreshQuiz();

            // 単語を更新 = 攻撃する
            int attackPower = _player.attackPower + (int)Mathf.Floor(_charCombo * 0.25f); // コンボ数を考慮した攻撃力を計算
            _enemyManager.TakeDamage(attackPower);
            _uiConnecter.WhenPlayerAttackToEnemy();
            
            _attackEffectManager.Rpc_AttackEffect(RoomPlayer.Local);

            // 単語を入力出来たら...
            _wordCombo++;

            if (_wordCombo % 10 == 0)
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
}
