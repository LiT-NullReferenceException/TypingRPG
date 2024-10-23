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

    // �L�[���͂��`�F�b�N���Đ��������ǂ������肷�郁�\�b�h
    bool CheckKeyInput(char inputedChar)
    {
        string inputKey = Input.inputString;

        // ���݂̂��肩�琳�𕶎����擾
        char correctChar = _quizManager.GetNowQuiz.roman[_quizManager.doneInputIndex];

        // ���͂��ꂽ�L�[�Ɛ��𕶎����r
        if (inputedChar == correctChar)
        {
            // Debug.Log("Correct key: " + inputKey);
            _quizManager.doneInputIndex++; // ���̕�����
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
        // �v���C���[����̂P�������͂��󂯕t����
        char inputedChar = _inputManager.GetChar();

        // �����͂̏ꍇ�̓��^�[��
        if (inputedChar == '\0') { return; }
        
        bool isCorrectChar = CheckKeyInput(inputedChar); // �����̓��͂�������
        bool isLastChar = (_quizManager.doneInputIndex == _quizManager.GetNowQuiz.roman.Length); // �Ō�̕������ǂ���

        // �Ō�̕����Ő����Ȃ�΁A�N�C�Y���X�V����
        if (isCorrectChar && isLastChar)
        {
            _quizManager.ChangeQuiz();
            _quizDisplayManager.ChangeDisplayQuizText(_quizManager.GetNowQuiz);
        }

    }
}
