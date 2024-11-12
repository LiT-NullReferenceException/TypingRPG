using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class JoinOrCreateRoomUI : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private TMP_InputField lobbyInput;
    [SerializeField] private UIScreen teamDialog;

    void Start()
    {
        audioManager.PlaySE(3);

        nicknameInput.onValueChanged.AddListener(x => ClientInfo.Username = x);
        lobbyInput.onValueChanged.AddListener(x => ServerInfo.LobbyName = x);
    }

    public void TryRoom()
    {
        RoomPlayer.PlayerJoined += FocusTeamDialog;
    }
    
    private void FocusTeamDialog(RoomPlayer player)
    {
        if (player == RoomPlayer.Local)     // 必要ない可能性あり
        {
            UIScreen.Focus(teamDialog);
            RoomPlayer.PlayerJoined -= FocusTeamDialog;
        }
    }
}
