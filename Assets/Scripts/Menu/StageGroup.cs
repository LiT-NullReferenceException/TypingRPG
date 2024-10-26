using System.Collections.Generic;
using UnityEngine;

public class StageGroup : MonoBehaviour
{
    private RectTransform _parentTransform;

    public void Init(List<StageData.TypingEnemy> enemies, StageSelectNode stageSelectNodePrefab, StageHelper stageHelper)
    {
        _parentTransform = GetComponent<RectTransform>();
        
        foreach (StageData.TypingEnemy enemy in enemies)
        {
            StageSelectNode stageSelectNode = Instantiate(stageSelectNodePrefab.gameObject, _parentTransform).GetComponent<StageSelectNode>();
            stageSelectNode.Init(enemy, stageHelper);
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
