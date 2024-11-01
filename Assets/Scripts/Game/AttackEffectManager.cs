using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class AttackEffectManager : MonoBehaviour
{
    List<RoomPlayer> dolls = new List<RoomPlayer>();
    //List<Transform> points = new List<Transform>();   こんな感じの座標のリストをここで取得したいです。

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
    public void AttackEffect(RoomPlayer player)
    {
        int index = dolls.FindIndex(roomPlayer => roomPlayer == player);
        //points[index].position = hogehoge;    // ここで座標のリストを元にエフェクトの出現位置を決める。
    }
}
