using TMPro;
using UnityEngine;

public class MatchFoundDialogView : MonoBehaviour, IDisabledUI
{
    [SerializeField] private TextMeshProUGUI _teamNameText;
    [SerializeField] private Transform _nameNodeParent;
    [SerializeField] private GameObject _nameNodePefab;

    public void Setup()
    {
        Debug.Log("MatchFoundDialogView.Setup");
        RoomPlayer.PlayerJoined += AddName;
    }

    public void OnDestruction()
    {
        
    }
    
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
    public void AddName(RoomPlayer _player)
    {
        Debug.Log("Adding name to room");
        GameObject nameNodeObj = Instantiate(_nameNodePefab, _nameNodeParent);
        NameNode nameNode = nameNodeObj.GetComponent<NameNode>();
        nameNode.SetName(_player.Username.Value);
    }
}
