using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RomanMapping
{
    public string Pattern; // �p�[�X�����Ƃ��̃p�^�[��
    public string[] TypePattern; // ���[�}�����͂̑ł���
}

public class RomanTypingParserJp : MonoBehaviour
{
    [SerializeField]
    private List<RomanMapping> _mappingList = new(); // �C���X�y�N�^�ŕҏW�\

    private Dictionary<string, string[]> _mappingDictionary = new();

    private void Awake()
    {
        // ���X�g�������ɕϊ�
        _mappingDictionary = _mappingList.ToDictionary(m => m.Pattern, m => m.TypePattern);
    }

    /// <summary>
    /// �Ђ炪�ȕ����p�[�X���Ĕ�����쐬
    /// </summary>
    /// <param name="sentenceHiragana">�p�[�X�����Ђ炪�ȕ�����</param>
    /// <returns>�p�[�X���ꂽ������Ɣ���I�[�g�}�g��</returns>
    public (List<string> parsedSentence, List<List<string>> judgeAutomaton) ConstructTypeSentence(string sentenceHiragana)
    {
        var idx = 0;
        var judge = new List<List<string>>();
        var parsedStr = new List<string>();

        while (idx < sentenceHiragana.Length)
        {
            List<string> validTypeList;

            var uni = sentenceHiragana[idx].ToString();
            var bi = (idx + 1 < sentenceHiragana.Length) ? sentenceHiragana.Substring(idx, 2) : "";
            var tri = (idx + 2 < sentenceHiragana.Length) ? sentenceHiragana.Substring(idx, 3) : "";

            if (_mappingDictionary.ContainsKey(tri))
            {
                validTypeList = _mappingDictionary[tri].ToList();
                idx += 3;
                parsedStr.Add(tri);
            }
            else if (_mappingDictionary.ContainsKey(bi))
            {
                validTypeList = _mappingDictionary[bi].ToList();
                idx += 2;
                parsedStr.Add(bi);
            }
            else if (_mappingDictionary.ContainsKey(uni))
            {
                validTypeList = _mappingDictionary[uni].ToList();
                idx++;
                parsedStr.Add(uni);
            }
            else
            {
                throw new InvalidOperationException($"Mapping data not found for: uni={uni}, bi={bi}, tri={tri}");
            }

            judge.Add(validTypeList);
        }

        return (parsedStr, judge);
    }
}
