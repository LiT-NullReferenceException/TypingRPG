using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class AttackEffectManager : NetworkBehaviour
{
    [SerializeField]List<RoomPlayer> dolls = new List<RoomPlayer>();
    [SerializeField] GameController gameController;
    [SerializeField] private AudioManager audioManager = null;

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
        
        // AudioManager を参照する
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_AttackEffect(RoomPlayer player)
    {
        audioManager.PlaySE(7);
        int index = dolls.FindIndex(roomPlayer => roomPlayer == player);
        gameController.dollPrefabs[index].GetComponent<PlayerAttackAnimator>()?.PlayEffect();
    }
}
