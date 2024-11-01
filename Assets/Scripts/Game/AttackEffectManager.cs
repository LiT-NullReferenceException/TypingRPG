using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class AttackEffectManager : MonoBehaviour
{
    [SerializeField]List<RoomPlayer> dolls = new List<RoomPlayer>();
    [SerializeField] GameController gameController;

    private void Awake()
    {
        foreach (RoomPlayer roomPlayer in RoomPlayer.Players)
        {
            if (roomPlayer == RoomPlayer.Local)
            {
                dolls.Add(roomPlayer);
            }
        }

        foreach (RoomPlayer roomPlayer in RoomPlayer.Players)
        {
            if (roomPlayer != RoomPlayer.Local)
            {
                dolls.Add(roomPlayer);
            }
        }

        foreach (RoomPlayer roomPlayer in dolls)
        {
            Debug.Log(roomPlayer.Username.Value);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_AttackEffect(RoomPlayer player)
    {
        int index = dolls.FindIndex(roomPlayer => roomPlayer == player);
        Vector3 pos = gameController.playersPosition[index];
        Debug.Log(pos);
        //points[index].position = hogehoge;    // ここで座標のリストを元にエフェクトの出現位置を決める。
    }
}
