using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizDataBase quizDataBase = null;
    [SerializeField] private Quiz nowQuiz;
    public Quiz GetNowQuiz { get { return nowQuiz; }  }

    // 何文字目まで正解したかを表す変数
    public int doneInputIndex = 0;
    //public int GetDoneInputIndex { get { return doneInputIndex; } }

    // 新しくクイズをデータセットから選び出す
    public void ChangeQuiz()
    {
        Quiz nextQuiz = quizDataBase.quizzes[Random.Range(0, quizDataBase.quizzes.Count)];

        SetQuiz(nextQuiz);
    }

    // nowQuizを更新する。
    private void SetQuiz(Quiz nextQuiz)
    {
        doneInputIndex = 0;
        this.nowQuiz = nextQuiz;
    }
}
