using System.Collections.Generic;
using UnityEngine;

public class StageGroup : MonoBehaviour
{
    private RectTransform _parentTransform;

    public void Init(List<StageData.Stage> stages, StageSelectNode stageSelectNodePrefab, StageHelper stageHelper)
    {
        _parentTransform = GetComponent<RectTransform>();
        
        foreach (StageData.Stage stage in stages)
        {
            StageSelectNode stageSelectNode = Instantiate(stageSelectNodePrefab.gameObject, _parentTransform).GetComponent<StageSelectNode>();
            stageSelectNode.Init(stage, stageHelper);
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
