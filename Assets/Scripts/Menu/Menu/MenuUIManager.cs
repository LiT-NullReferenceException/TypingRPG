using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuUIManager : MonoBehaviour
{
    private int num = 0;
    [SerializeField] private GameObject StartPanel;
    
    [SerializeField] private GameObject CreateSettingPanel;
    [SerializeField] private GameObject JoinSettingPanel;
    
    [SerializeField] private GameObject SearchingPanel;
    
    [SerializeField] private GameObject HostReadyPanel;
    [SerializeField] private GameObject GuestReadyPanel;
    
    [SerializeField] private GameObject SelectPanel;
    
    [SerializeField] private GameObject ToGamePanel;
    
    private TMP_InputField nicknameInput;
    private TMP_InputField lobbyInput;
    [SerializeField] private TMP_InputField hostNameInput;
    [SerializeField] private TMP_InputField guestNameInput;
    [SerializeField] private TMP_InputField hostLobbyInput;
    [SerializeField] private TMP_InputField guestLobbyInput;
    
    [SerializeField] private GameObject backGround;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopCreateUI()
    {
        CreateSettingPanel.SetActive(true);
        nicknameInput = hostNameInput;
        nicknameInput.onValueChanged.AddListener(x => ClientInfo.Username = x);
        lobbyInput = hostLobbyInput;
        lobbyInput.onValueChanged.AddListener(x => ServerInfo.LobbyName = x);
        SwitchUI();
    }
    
    public void PopJoinUI()
    {
        JoinSettingPanel.SetActive(true);
        nicknameInput = guestNameInput;
        nicknameInput.onValueChanged.AddListener(x => ClientInfo.Username = x);
        lobbyInput = guestLobbyInput;
        lobbyInput.onValueChanged.AddListener(x => ServerInfo.LobbyName = x);
        SwitchUI();
    }

    public async void SwitchUI()
    {
        if (num == 0)
        {
            StartPanel.SetActive(false);
            num++;
        }
        else if (num == 1)
        {
            CreateSettingPanel.SetActive(false);
            JoinSettingPanel.SetActive(false);
            
            SearchingPanel.SetActive(true);
            
            // 非同期で RoomPlayer.Local を待つ場合は、適切な方法で null チェックを行います
            while (RoomPlayer.Local == null)
            {
                await Task.Yield();  // これで `RoomPlayer.Local` が null でなくなるまで待機
            }
            
            SearchingPanel.SetActive(false);
            if (RoomPlayer.Local.IsLeader)
            {
                HostReadyPanel.SetActive(true);
                HostReadyPanel.GetComponent<MatchFoundDialogView>().SetTeamName(ServerInfo.LobbyName);
                num++;
            }
            else
            {
                GuestReadyPanel.SetActive(true);
                GuestReadyPanel.GetComponent<MatchFoundDialogView>().SetTeamName(ServerInfo.LobbyName);
            }
        }
        else if (num == 2)
        {
            HostReadyPanel.SetActive(false);
            SelectPanel.SetActive(true);
            RoomPlayer.Local.IsReady = true;
            num++;
        }
        else if (num == 3)
        {
            ToGamePanel.SetActive(true);
            num++;
        }
        else if (num == 4)
        {
            // SelectPanel.SetActive(false);
            // backGround.SetActive(false);
        }
        
    }
}