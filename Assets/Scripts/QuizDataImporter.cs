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
        // JSON�t�@�C���̃p�X
        string jsonFilePath = Application.dataPath + "/Resources/kana_kanji_data.json";

        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError("JSON file not found: " + jsonFilePath);
            return;
        }

        // JSON�t�@�C���̓ǂݍ���
        string jsonData = File.ReadAllText(jsonFilePath);

        // JSON���f�V���A���C�Y
        List<Quiz> quizzes = JsonUtility.FromJson<QuizListWrapper>("{\"quizzes\":" + jsonData + "}").quizzes;

        // �X�N���v�^�u���I�u�W�F�N�g�̐���
        QuizDataBase quizDatabase = ScriptableObject.CreateInstance<QuizDataBase>();
        quizDatabase.quizzes = quizzes;

        // �A�Z�b�g�Ƃ��ĕۑ�
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

