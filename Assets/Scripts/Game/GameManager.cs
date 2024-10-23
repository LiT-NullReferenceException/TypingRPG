using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private QuizManager _quizManager = null;

    [SerializeField] private InputManager _inputManager = null;

    [SerializeField] private QuizDisplayManager _quizDisplayManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _quizManager.ChangeQuiz();

        _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);
    }

    // キー入力をチェックして正しいかどうか判定するメソッド
    bool CheckKeyInput(char inputedChar)
    {
        string inputKey = Input.inputString;

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


    // Update is called once per frame
    void Update()
    {
        // プレイヤーからの１文字入力を受け付ける
        char inputedChar = _inputManager.GetChar();

        // 無入力の場合はリターン
        if (inputedChar == '\0') { return; }
        
        bool isCorrectChar = CheckKeyInput(inputedChar); // 正解の入力だったか
        bool isLastChar = (_quizManager.doneInputIndex == _quizManager.GetNowQuiz.roman.Length); // 最後の文字かどうか

        // 最後の文字で正解ならば、クイズを更新する
        if (isCorrectChar && isLastChar)
        {
            _quizManager.ChangeQuiz();
            _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);
        }

    }
}
