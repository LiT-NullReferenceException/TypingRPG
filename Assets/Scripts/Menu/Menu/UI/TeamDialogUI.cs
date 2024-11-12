using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDialogUI : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MatchFoundDialogView>().SetTeamName(ServerInfo.LobbyName);
    }

    public void GetReady()
    {
        audioManager.PlaySE(4);
        RoomPlayer.Local.IsReady = true;
    }
}
