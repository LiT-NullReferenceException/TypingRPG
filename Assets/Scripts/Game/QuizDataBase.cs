using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuizDataBase")]
public class QuizDataBase:ScriptableObject
{
    public List<Quiz> quizzes = new List<Quiz>();
}

[System.Serializable]
public struct Quiz
{
    public string original;
    public string japanese;
    public string roman;
}
