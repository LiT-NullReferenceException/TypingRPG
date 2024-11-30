using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class QuizDataImporter : MonoBehaviour
{
    [System.Serializable]
    public class QuizList
    {
        public List<Quiz> quizzes;
    }

    [MenuItem("Tools/Import Quiz JSON to ScriptableObject")]
    public static void ImportQuizData()
    {
        // JSONファイルのパス
        string jsonFilePath = Application.dataPath + "/Resources/kana_kanji_data.json";

        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("JSON file not found: " + jsonFilePath);
            return;
        }

        // JSONファイルの読み込み
        string jsonData = File.ReadAllText(jsonFilePath);

        // JSONをデシリアライズ
        List<Quiz> quizzes = JsonUtility.FromJson<QuizListWrapper>("{\"quizzes\":" + jsonData + "}").quizzes;

        // スクリプタブルオブジェクトの生成
        QuizDataBase quizDatabase = ScriptableObject.CreateInstance<QuizDataBase>();
        quizDatabase.quizzes = quizzes;

        // アセットとして保存
        string assetPath = "Assets/Resources/QuizDataBase.asset";
        AssetDatabase.CreateAsset(quizDatabase, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log("QuizDataBase asset created at: " + assetPath);
    }

    [System.Serializable]
    private class QuizListWrapper
    {
        public List<Quiz> quizzes;
    }

    private void Start()
    {
        ImportQuizData();
    }
}

