using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TypingGame : MonoBehaviour
{
    [SerializeField] private TextAsset dictionaryFile;

    private List<DictionaryEntry> conversionDictionary;
    private List<List<string>> romanPatterns; // ���݂̓��͂ɑΉ����郍�[�}���p�^�[��
    private int currentIndex; // ���ݔ��蒆�̕����C���f�b�N�X
    private string currentInput; // ���[�U�[�̌��݂̓���
    private bool isComplete; // ���ׂē��͂���������������

    private void Start()
    {
        LoadDictionary();

        string targetText = "�炢�ӂ����Ă���"; // �T���v������
        romanPatterns = ConvertToRomanPatterns(targetText);
        ResetGame();

        Debug.Log("�^�[�Q�b�g: " + targetText);
        DebugPatterns();
    }

    // �����f�[�^�����[�h
    private void LoadDictionary()
    {
        if (dictionaryFile == null)
        {
            Debug.LogError("�����t�@�C�����w�肳��Ă��܂���B");
            return;
        }

        string json = dictionaryFile.text;
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        conversionDictionary = new List<DictionaryEntry>(wrapper.entries);
    }

    // JSON�f�V���A���C�Y�p
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

    // �Ђ炪�Ȃ����[�}���p�^�[���ɕϊ�
    private List<List<string>> ConvertToRomanPatterns(string input)
    {
        List<List<string>> result = new List<List<string>>();
        int i = 0;

        while (i < input.Length)
        {
            bool foundMatch = false;

            // ���������i2�����ȏ�j���Ƀ`�F�b�N
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

    // �Q�[����Ԃ����Z�b�g
    private void ResetGame()
    {
        currentIndex = 0;
        currentInput = "";
        isComplete = false;
    }

    // ���͂�����
    public void HandleInput(char inputChar)
    {
        if (isComplete) return;

        currentInput += inputChar;

        if (IsCurrentInputValid())
        {
            Debug.Log($"����: {currentInput}");

            // ���݂̓��͂����S��v�����玟�֐i��
            if (IsCurrentInputComplete())
            {
                currentIndex++;
                currentInput = "";

                if (currentIndex >= romanPatterns.Count)
                {
                    Debug.Log("�S�Đ������܂����I");
                    isComplete = true;
                }
            }
        }
        else
        {
            Debug.Log($"�s����: {currentInput}");
            // ���s�������͂��Ȃ��������Ƃɂ���
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
        }
    }

    // ���݂̓��͂��L���ȃ��[�}���p�^�[���Ɉ�v���Ă��邩
    private bool IsCurrentInputValid()
    {
        if (currentIndex >= romanPatterns.Count) return false;

        return romanPatterns[currentIndex].Any(pattern => pattern.StartsWith(currentInput));
    }

    // ���݂̓��͂����S��v������
    private bool IsCurrentInputComplete()
    {
        if (currentIndex >= romanPatterns.Count) return false;

        return romanPatterns[currentIndex].Contains(currentInput);
    }

    // �p�^�[�����f�o�b�O�\��
    private void DebugPatterns()
    {
        Debug.Log("�ϊ��p�^�[��:");
        foreach (var patternList in romanPatterns)
        {
            Debug.Log(string.Join(", ", patternList));
        }
    }
    private void Update()
    {
        char input = GetChar();

        if(input == '\0') { return; }

        HandleInput(input);
    }

    public char GetChar()
    {
        // �A���t�@�x�b�g�̃L�[�ɑΉ����镶�����i�[����ϐ�
        char input = '\0';

        // A-Z�L�[�𒼐ڃ`�F�b�N
        if (Input.GetKeyDown(KeyCode.A)) input = 'a';
        else if (Input.GetKeyDown(KeyCode.B)) input = 'b';
        else if (Input.GetKeyDown(KeyCode.C)) input = 'c';
        else if (Input.GetKeyDown(KeyCode.D)) input = 'd';
        else if (Input.GetKeyDown(KeyCode.E)) input = 'e';
        else if (Input.GetKeyDown(KeyCode.F)) input = 'f';
        else if (Input.GetKeyDown(KeyCode.G)) input = 'g';
        else if (Input.GetKeyDown(KeyCode.H)) input = 'h';
        else if (Input.GetKeyDown(KeyCode.I)) input = 'i';
        else if (Input.GetKeyDown(KeyCode.J)) input = 'j';
        else if (Input.GetKeyDown(KeyCode.K)) input = 'k';
        else if (Input.GetKeyDown(KeyCode.L)) input = 'l';
        else if (Input.GetKeyDown(KeyCode.M)) input = 'm';
        else if (Input.GetKeyDown(KeyCode.N)) input = 'n';
        else if (Input.GetKeyDown(KeyCode.O)) input = 'o';
        else if (Input.GetKeyDown(KeyCode.P)) input = 'p';
        else if (Input.GetKeyDown(KeyCode.Q)) input = 'q';
        else if (Input.GetKeyDown(KeyCode.R)) input = 'r';
        else if (Input.GetKeyDown(KeyCode.S)) input = 's';
        else if (Input.GetKeyDown(KeyCode.T)) input = 't';
        else if (Input.GetKeyDown(KeyCode.U)) input = 'u';
        else if (Input.GetKeyDown(KeyCode.V)) input = 'v';
        else if (Input.GetKeyDown(KeyCode.W)) input = 'w';
        else if (Input.GetKeyDown(KeyCode.X)) input = 'x';
        else if (Input.GetKeyDown(KeyCode.Y)) input = 'y';
        else if (Input.GetKeyDown(KeyCode.Z)) input = 'z';
        else if (Input.GetKeyDown(KeyCode.Minus)) input = '-';

        return input; // �v���C���[�̓��͂�Ԃ�
    }
}
