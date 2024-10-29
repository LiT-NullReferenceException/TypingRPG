using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    private int num = 0;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject CreateSettingPanel;
    [SerializeField] private GameObject JoinSettingPanel;
    [SerializeField] private GameObject SearchingPanel;
    [SerializeField] private GameObject ReadyPanel;
    [SerializeField] private GameObject SelectPanel;
    [SerializeField] private GameObject ToGamePanel;
    
    private TMP_InputField nicknameInput;
    [SerializeField] private TMP_InputField hostInput;
    [SerializeField] private TMP_InputField guestInput;
    // Start is called before the first frame update
    void Start()
    {
        nicknameInput.onValueChanged.AddListener(x => ClientInfo.Username = x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopCreateUI()
    {
        CreateSettingPanel.SetActive(true);
        nicknameInput = hostInput;
        SwitchUI();
    }
    
    public void PopJoinUI()
    {
        JoinSettingPanel.SetActive(true);
        nicknameInput = guestInput;
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
            
            // 非同期で RoomPlayer.Local を待つ場合は、適切な方法で null チェックを行います
            while (RoomPlayer.Local == null)
            {
                await Task.Yield();  // これで `RoomPlayer.Local` が null でなくなるまで待機
            }
            
            if (RoomPlayer.Local.IsLeader)
            {
                ReadyPanel.SetActive(true);
            }
            num++;
        }
        else if (num == 2)
        {
            ReadyPanel.SetActive(false);
            SelectPanel.SetActive(true);
            num++;
        }
        else if (num == 3)
        {
            SelectPanel.SetActive(false);
            ToGamePanel.SetActive(true);
            num++;
        }
        else if (num == 4)
        {
            ToGamePanel.SetActive(false);
        }
        
    }
}