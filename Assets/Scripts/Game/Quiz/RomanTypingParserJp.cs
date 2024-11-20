using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class RomanMapping
{
    public string Pattern; // パースされるときのパターン
    public string[] TypePattern; // ローマ字入力の打ち方
}

public class RomanTypingParserJp : MonoBehaviour
{
    [SerializeField]
    private List<RomanMapping> _mappingList = new(); // インスペクタで編集可能

    private Dictionary<string, string[]> _mappingDictionary = new();

    private void Awake()
    {
        // リストを辞書に変換
        _mappingDictionary = _mappingList.ToDictionary(m => m.Pattern, m => m.TypePattern);
    }

    /// <summary>
    /// ひらがな文をパースして判定を作成
    /// </summary>
    /// <param name="sentenceHiragana">パースされるひらがな文字列</param>
    /// <returns>パースされた文字列と判定オートマトン</returns>
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
