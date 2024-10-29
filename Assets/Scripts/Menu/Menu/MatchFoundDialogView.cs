using TMPro;
using UnityEngine;

public class MatchFoundDialogView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _teamNameText;
    [SerializeField] private Transform _nameNodeParent;
    [SerializeField] private GameObject _nameNodePefab;

    /// <summary>
    /// チーム名を表示する
    /// </summary>
    /// <param name="teamName"></param>
    public void SetTeamName(string teamName)
    {
        _teamNameText.text = teamName;
    }

    /// <summary>
    /// チームに所属するメンバー名を追加・表示する
    /// </summary>
    /// <param name="name"></param>
    public void AddName(string name)
    {
        GameObject nameNodeObj = Instantiate(_nameNodePefab, _nameNodeParent);
        NameNode nameNode = nameNodeObj.GetComponent<NameNode>();
        nameNode.SetName(name);
    }
}
