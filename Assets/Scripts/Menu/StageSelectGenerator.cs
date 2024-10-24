using UnityEngine;

public class StageSelectGenerator : MonoBehaviour
{
    [SerializeField] private StageData _stageData;

    [SerializeField] private Transform _stageSelectNodeParent;
    [SerializeField] private StageSelectNode _stageSelectNode;
    [SerializeField] private StageHelper _stageHelper;

    private void Awake()
    {
        Generate();
    }

    /// <summary>
    /// ステージ選択ボタンを生成する
    /// </summary>
    public void Generate()
    {
        for (int i = 0; i < _stageData.Enemies.Count; i++)
        {
            StageSelectNode stageSelectNode = Instantiate(_stageSelectNode.gameObject, _stageSelectNodeParent).GetComponent<StageSelectNode>();
            stageSelectNode.Init(_stageData.Enemies[i], _stageHelper);
        }
    }
}
