using System;
using System.Collections.Generic;
using UnityEngine;

public class HiraganaToRomanConverter : MonoBehaviour
{
    // �C���X�y�N�^�Ŏ����t�@�C�����w��
    [SerializeField] private TextAsset dictionaryFile;

    // �����f�[�^��ێ����郊�X�g
    private List<DictionaryEntry> conversionDictionary;

    // ���������[�h����
    private void LoadDictionary()
    {
        if (dictionaryFile == null)
        {
            Debug.LogError("�����t�@�C�����w�肳��Ă��܂���B");
            return;
        }

        // JSON�t�@�C�����f�V���A���C�Y
        string json = dictionaryFile.text;
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        conversionDictionary = new List<DictionaryEntry>(wrapper.entries);

        Debug.Log("�����t�@�C�������[�h���܂����B");
    }

    // JSON���f�V���A���C�Y���邽�߂̃��b�p�[�N���X
    [Serializable]
    private class Wrapper
    {
        public DictionaryEntry[] entries;
    }

    // �����G���g���̃N���X
    [Serializable]
    private class DictionaryEntry
    {
        public string Pattern;
        public List<string> TypePattern;
    }

    // �Ђ炪�Ȃ����[�}���p�^�[���ɕϊ�
    public List<List<string>> ConvertToRomanPatterns(string input)
    {
        if (conversionDictionary == null)
        {
            Debug.LogError("���������[�h����Ă��܂���B");
            return null;
        }

        List<List<string>> result = new List<List<string>>();
        int i = 0;

        while (i < input.Length)
        {
            bool foundMatch = false;

            // ���������i2�����ȏ�j���Ƀ`�F�b�N
            foreach (var entry in conversionDictionary)
            {
                // 2�����ȏ�̕������ŏ��Ƀ`�F�b�N
                if (i + 1 < input.Length && input.Substring(i, 2) == entry.Pattern)
                {
                    result.Add(new List<string>(entry.TypePattern));
                    i += 2;  // 2�����i��
                    foundMatch = true;
                    break;
                }
                else if (i + 2 < input.Length && input.Substring(i, 3) == entry.Pattern)
                {
                    result.Add(new List<string>(entry.TypePattern));
                    i += 3;  // 3�����i��
                    foundMatch = true;
                    break;
                }
            }

            // ����������������Ȃ������ꍇ�A�P�Ƃ�1����������
            if (!foundMatch)
            {
                string kana = input[i].ToString();
                var entry = conversionDictionary.Find(e => e.Pattern == kana);

                List<string> romanPatterns = new List<string>();
                if (entry != null)
                {
                    romanPatterns.AddRange(entry.TypePattern);
                }
                else
                {
                    // �Ή����郍�[�}���p�^�[�����Ȃ��ꍇ�́A���̂܂܂̕�����ǉ�
                    romanPatterns.Add(kana);
                }

                result.Add(romanPatterns);
                i++;  // 1�����i��
            }
        }

        return result;
    }

    private void Awake()
    {
        LoadDictionary(); // ���������[�h
    }

    //[SerializeField] private string input = string.Empty;

    //// �e�X�g�p
    //private void Start()
    //{
    //    //input = "�����������ӂ��񂫂�"; // ��: "�����������ӂ��񂫂�"

    //    List<List<string>> patterns = ConvertToRomanPatterns(input);

    //    if (patterns != null)
    //    {
    //        Debug.Log("�ϊ�����:");
    //        foreach (var pattern in patterns)
    //        {
    //            string romanPattern = string.Join(", ", pattern);
    //            Debug.Log(romanPattern);
    //        }
    //    }
    //}
}
