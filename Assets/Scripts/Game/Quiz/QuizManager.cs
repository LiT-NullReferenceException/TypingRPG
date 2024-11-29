using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]
class Difficulty
{
    [Header("難易度"), Range(2, 14)]
    public int index = 0;
}

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizDataBase quizDataBase = null;
    [SerializeField] private Quiz nowQuiz;
    public Quiz GetNowQuiz { get { return nowQuiz; }  }

    // 何文字目まで正解したかを表す変数
    public int doneInputIndex = 0;
    //public int GetDoneInputIndex { get { return doneInputIndex; } }

    // ランダムモードかどうか
    [SerializeField] private bool isRandom = true;
    // 順番モードの時のインデックス
    [SerializeField] private int quizIndex = 0;

    [Header("難易度のリスト")]
    [SerializeField] private List<Difficulty> difficultyIndex = new List<Difficulty>();

    private int GetIndexByDifficulty(int difficulty)
    {
        int index = 0;

        switch (difficulty)
        {
            case  2 : index = Random.Range(   0,   58); break;
            case  3 : index = Random.Range(  59,  151); break;
            case  4 : index = Random.Range( 152,  271); break;
            case  5 : index = Random.Range( 272,  431); break;
            case  6 : index = Random.Range( 432,  614); break;
            case  7 : index = Random.Range( 615,  835); break;
            case  8 : index = Random.Range( 836, 1028); break;
            case  9 : index = Random.Range(1029, 1205); break;
            case 10 : index = Random.Range(1206, 1332); break;
            case 11 : index = Random.Range(1333, 1439); break;
            case 12 : index = Random.Range(1440, 1544); break;
            case 13 : index = Random.Range(1545, 1640); break;
            case 14 : index = Random.Range(1641, 1757); break; // 14文字以上
            default : return index = Random.Range(0, quizDataBase.quizzes.Count); // ランダムにピックアップ
        }

        return index;
    }


    // 新しくクイズをデータセットから選び出す
    public void ChangeQuiz()
    {
        Quiz nextQuiz;

        if (difficultyIndex == null) 
        {
            // 難易度リストがないとき

            nextQuiz = quizDataBase.quizzes[Random.Range(0, quizDataBase.quizzes.Count)];
        }
        else 
        {
            // 難易度リストがあるとき

            Debug.Log("OKOKOKOKOKOKOKOKKO");

            if (isRandom)
            {
                // 難易度のリストからランダムに難易度を選択
                int difficulty = difficultyIndex[Random.Range(0, difficultyIndex.Count)].index;
                // 難易度を元にお題をランダムに選択
                int index = GetIndexByDifficulty(difficulty);

                nextQuiz = quizDataBase.quizzes[index];
            }
            else
            {
                nextQuiz = quizDataBase.quizzes[quizIndex];
                quizIndex++;
            }
        }

        SetQuiz(nextQuiz);
    }

    // nowQuizを更新する。
    private void SetQuiz(Quiz nextQuiz)
    {
        doneInputIndex = 0;
        this.nowQuiz = nextQuiz;
    }
}
