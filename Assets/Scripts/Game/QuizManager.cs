using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuizDataBase quizDataBase = null;
    [SerializeField] private Quiz nowQuiz;
    public Quiz GetNowQuiz { get { return nowQuiz; } }

    // �������ڂ܂Ő�����������\���ϐ�
    public int doneInputIndex = 0;
    //public int GetDoneInputIndex { get { return doneInputIndex; } }

    // �V�����N�C�Y���f�[�^�Z�b�g����I�яo��
    public void ChangeQuiz()
    {
        Quiz nextQuiz = quizDataBase.quizzes[Random.Range(0, quizDataBase.quizzes.Count)];

        SetQuiz(nextQuiz);
    }

    // nowQuiz���X�V����B
    private void SetQuiz(Quiz nextQuiz)
    {
        doneInputIndex = 0;
        this.nowQuiz = nextQuiz;
    }
}
