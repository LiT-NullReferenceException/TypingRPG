using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class GuestMessage : MonoBehaviour
{
    private TMP_Text messageText;

    private void Awake()
    {
        messageText = GetComponent<TMP_Text>();
        RoomPlayer.PlayerChanged += Rpc_ChangeMessage;
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    public void Rpc_ChangeMessage(RoomPlayer roomPlayer)
    {
        if (roomPlayer.IsReady)
        {
            messageText.text = "ホストがステージを選んでいます";
        }
        else
        {
            messageText.text = "メンバー集結中...";
        }
    }
}
