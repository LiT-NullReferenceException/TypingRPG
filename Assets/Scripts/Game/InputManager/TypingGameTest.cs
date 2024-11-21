using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TypingGame : MonoBehaviour
{
    [SerializeField] private TextAsset dictionaryFile;

    private List<DictionaryEntry> conversionDictionary;
    private List<List<string>> romanPatterns; // 現在の入力に対応するローマ字パターン
    private int currentIndex; // 現在判定中の文字インデックス
    private string currentInput; // ユーザーの現在の入力
    private bool isComplete; // すべて入力が完了したか判定

    private void Start()
    {
        LoadDictionary();

        string targetText = "らいふいずてっく"; // サンプル入力
        romanPatterns = ConvertToRomanPatterns(targetText);
        ResetGame();

        Debug.Log("ターゲット: " + targetText);
        DebugPatterns();
    }

    // 辞書データをロード
    private void LoadDictionary()
    {
        if (dictionaryFile == null)
        {
            Debug.LogError("辞書ファイルが指定されていません。");
            return;
        }

        string json = dictionaryFile.text;
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        conversionDictionary = new List<DictionaryEntry>(wrapper.entries);
    }

    // JSONデシリアライズ用
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

    // ひらがなをローマ字パターンに変換
    private List<List<string>> ConvertToRomanPatterns(string input)
    {
        List<List<string>> result = new List<List<string>>();
        int i = 0;

        while (i < input.Length)
        {
            bool foundMatch = false;

            // 複合文字（2文字以上）を先にチェック
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

    // ゲーム状態をリセット
    private void ResetGame()
    {
        currentIndex = 0;
        currentInput = "";
        isComplete = false;
    }

    // 入力を処理
    public void HandleInput(char inputChar)
    {
        if (isComplete) return;

        currentInput += inputChar;

        if (IsCurrentInputValid())
        {
            Debug.Log($"正解: {currentInput}");

            // 現在の入力が完全一致したら次へ進む
            if (IsCurrentInputComplete())
            {
                currentIndex++;
                currentInput = "";

                if (currentIndex >= romanPatterns.Count)
                {
                    Debug.Log("全て正解しました！");
                    isComplete = true;
                }
            }
        }
        else
        {
            Debug.Log($"不正解: {currentInput}");
            // 失敗した入力をなかったことにする
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
        }
    }

    // 現在の入力が有効なローマ字パターンに一致しているか
    private bool IsCurrentInputValid()
    {
        if (currentIndex >= romanPatterns.Count) return false;

        return romanPatterns[currentIndex].Any(pattern => pattern.StartsWith(currentInput));
    }

    // 現在の入力が完全一致したか
    private bool IsCurrentInputComplete()
    {
        if (currentIndex >= romanPatterns.Count) return false;

        return romanPatterns[currentIndex].Contains(currentInput);
    }

    // パターンをデバッグ表示
    private void DebugPatterns()
    {
        Debug.Log("変換パターン:");
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
        // アルファベットのキーに対応する文字を格納する変数
        char input = '\0';

        // A-Zキーを直接チェック
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

        return input; // プレイヤーの入力を返す
    }
}
