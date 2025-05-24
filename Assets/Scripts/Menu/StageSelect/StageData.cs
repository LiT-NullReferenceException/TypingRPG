using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    public List<Stage> Stages = new List<Stage>();

    [System.Serializable]
    public class Stage
    {
        public string name;
        public int level;
        public TypingEnemy[] typingEnemies;
        [SerializeField]
        public TypingEnemy mainEnemy => typingEnemies[0];
    }
    
    [System.Serializable]
    public class TypingEnemy
    {
        public string name;
        public Sprite sprite;
        public int maxHealth;
        public int attackPower;
    }
}