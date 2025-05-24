// Assets/Editor/QuizDataImporter.cs
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class QuizDataImporter
{
    [MenuItem("Tools/Import Quiz JSON to ScriptableObject")]
    public static void ImportQuizData()
    {
        var jsonPath = Path.Combine(Application.dataPath, "Resources/kana_kanji_data.json");
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("JSON file not found: " + jsonPath);
            return;
        }

        string jsonData = File.ReadAllText(jsonPath);
        var wrapper = JsonUtility.FromJson<QuizListWrapper>("{\"quizzes\":" + jsonData + "}");
        
        var db = ScriptableObject.CreateInstance<QuizDataBase>();
        db.quizzes = wrapper.quizzes;

        const string assetPath = "Assets/Resources/QuizDataBase.asset";
        AssetDatabase.CreateAsset(db, assetPath);
        AssetDatabase.SaveAssets();
        
        Debug.Log("QuizDataBase asset created at: " + assetPath);
    }

    [System.Serializable]
    private class QuizListWrapper
    {
        public List<Quiz> quizzes;
    }
}
#endif