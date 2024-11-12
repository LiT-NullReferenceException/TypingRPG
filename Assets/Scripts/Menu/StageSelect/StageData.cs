using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    public List<TypingEnemy> Enemies = new List<TypingEnemy>();
    
    [System.Serializable]
    public class TypingEnemy
    {
        public string name;
        public Sprite sprite;
        public int level;
    }
}