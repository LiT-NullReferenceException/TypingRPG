using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private QuizManager _quizManager = null;

    [SerializeField] private InputManager _inputManager = null;

    [SerializeField] private QuizDisplayManager _quizDisplayManager = null;

    [SerializeField] private EnemyManager _enemyManager = null;

    [SerializeField] private Player _player = null;

    [SerializeField] private HPBarManager _hpBarManager = null;

    [SerializeField] private UIConnecter _uiConnecter = null;

    // Start is called before the first frame update
    void Start()
    {
        _quizManager.ChangeQuiz();
        _uiConnecter.WhenRefreshQuiz();

        _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);

        // 敵キャラを管理するスクリプトを取得（これは全プレイヤーで１つを共有するため、直接参照するのは危ない）
        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();

        // HPバーを更新する
        _hpBarManager.UpdatePlayerHPBar(_player.maxHealth, _player.health);
        _hpBarManager.UpdataEnemyHPBar();
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
        if (_player.status != Character.Status.alive) { return; }

        if (_enemyManager.status == EnemyManager.Status.done) { return; }

        // プレイヤーからの１文字入力を受け付ける
        char inputedChar = _inputManager.GetChar();

        // 無入力の場合はリターン
        if (inputedChar == '\0') { return; }
        
        bool isCorrectChar = CheckKeyInput(inputedChar); // 正解の入力だったか
        bool isLastChar = (_quizManager.doneInputIndex == _quizManager.GetNowQuiz.roman.Length); // 最後の文字かどうか

        if (isCorrectChar) 
        {
            _charCombo++;
            _uiConnecter.WhenCharComboIncreased();
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

            // 単語を入力出来たら...
            _wordCombo++;

            if (_wordCombo % 10 == 0)
            {
                // ブーストを発火
                _enemyManager.SetIsBoosting = true;
            }
        }

        if (!isCorrectChar)
        {
            // モンスターから攻撃される
            _player.TakeDamage(_enemyManager.GetAttackPower());
            _uiConnecter.WhenEnemyAttackToPlayer();

            // プレイヤーのHPバーを更新する
            _hpBarManager.UpdatePlayerHPBar(_player.maxHealth, _player.health);

            // コンボカウンターのリセット
            _charCombo = 0;
            _wordCombo = 0;
            _uiConnecter.WhenCharComboDecreased();

        }

    }
}
