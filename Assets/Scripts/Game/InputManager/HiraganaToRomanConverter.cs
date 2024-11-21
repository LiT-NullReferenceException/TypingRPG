using System;
using System.Collections.Generic;
using UnityEngine;

public class HiraganaToRomanConverter : MonoBehaviour
{
    // インスペクタで辞書ファイルを指定
    [SerializeField] private TextAsset dictionaryFile;

    // 辞書データを保持するリスト
    private List<DictionaryEntry> conversionDictionary;

    // 辞書をロードする
    private void LoadDictionary()
    {
        if (dictionaryFile == null)
        {
            Debug.LogError("辞書ファイルが指定されていません。");
            return;
        }

        // JSONファイルをデシリアライズ
        string json = dictionaryFile.text;
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        conversionDictionary = new List<DictionaryEntry>(wrapper.entries);

        Debug.Log("辞書ファイルをロードしました。");
    }

    // JSONをデシリアライズするためのラッパークラス
    [Serializable]
    private class Wrapper
    {
        public DictionaryEntry[] entries;
    }

    // 辞書エントリのクラス
    [Serializable]
    private class DictionaryEntry
    {
        public string Pattern;
        public List<string> TypePattern;
    }

    // ひらがなをローマ字パターンに変換
    public List<List<string>> ConvertToRomanPatterns(string input)
    {
        if (conversionDictionary == null)
        {
            Debug.LogError("辞書がロードされていません。");
            return null;
        }

        List<List<string>> result = new List<List<string>>();
        int i = 0;

        while (i < input.Length)
        {
            bool foundMatch = false;

            // 複合文字（2文字以上）を先にチェック
            foreach (var entry in conversionDictionary)
            {
                // 2文字以上の文字を最初にチェック
                if (i + 1 < input.Length && input.Substring(i, 2) == entry.Pattern)
                {
                    result.Add(new List<string>(entry.TypePattern));
                    i += 2;  // 2文字進む
                    foundMatch = true;
                    break;
                }
                else if (i + 2 < input.Length && input.Substring(i, 3) == entry.Pattern)
                {
                    result.Add(new List<string>(entry.TypePattern));
                    i += 3;  // 3文字進む
                    foundMatch = true;
                    break;
                }
            }

            // 複合文字が見つからなかった場合、単独の1文字を処理
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
                    // 対応するローマ字パターンがない場合は、そのままの文字を追加
                    romanPatterns.Add(kana);
                }

                result.Add(romanPatterns);
                i++;  // 1文字進む
            }
        }

        return result;
    }

    private void Awake()
    {
        LoadDictionary(); // 辞書をロード
    }

    //[SerializeField] private string input = string.Empty;

    //// テスト用
    //private void Start()
    //{
    //    //input = "あしたっくふぃんきゃ"; // 例: "あしたっくふぃんきゃ"

    //    List<List<string>> patterns = ConvertToRomanPatterns(input);

    //    if (patterns != null)
    //    {
    //        Debug.Log("変換結果:");
    //        foreach (var pattern in patterns)
    //        {
    //            string romanPattern = string.Join(", ", pattern);
    //            Debug.Log(romanPattern);
    //        }
    //    }
    //}
}
