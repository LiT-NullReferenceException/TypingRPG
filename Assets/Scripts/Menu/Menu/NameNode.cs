using TMPro;
using UnityEngine;

public class NameNode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    private RoomPlayer _player;
    
    public void SetName(RoomPlayer player)
    {
        Debug.Log("nameSet!");
        _player = player;
        _nameText.text = _player.Username.Value;
    }
    
    private void Update() {
        if (_player.Object != null && _player.Object.IsValid)
        {
            _nameText.text = _player.Username.Value;
        }
    }
}
