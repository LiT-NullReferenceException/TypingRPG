using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class TeamDialogUI : MonoBehaviour
{
    //[SerializeField] private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!ServerInfo.LobbyName.IsNullOrEmpty())
        {
            GetComponent<MatchFoundDialogView>().SetTeamName(ServerInfo.LobbyName);
        }
        else
        {
            GetComponent<MatchFoundDialogView>().SetTeamName(ClientInfo.LobbyName);
        }
        
    }

    public void GetReady()
    {
        AudioManager.instance_AudioManager.PlaySE(4);
        RoomPlayer.Local.IsReady = true;
    }
}
