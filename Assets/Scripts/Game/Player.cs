using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class Player : Character
{
    [Networked] public RoomPlayer RoomUser { get; set; }
    [Networked] public int AlivePlayers { get; private set; }
    public String name;
    
    public static bool allPlayersDead = false; 
    
    public override void Spawned()
    {
        base.Spawned();
        //characterName = "Player";
        health = maxHealth;
        //attackPower = 20;

        AlivePlayers = RoomPlayer.Players.Count;
    }

    // プレイヤーの特殊な攻撃などをここに追加可能
    public override void Attack(Character target)
    {
        base.Attack(target);
        // 追加効果があればここで処理する
    }

    public override void Die()
    {
        status = Status.dead;
        Rpc_DecreaseAlivePlayers();

        Debug.Log("[Player - Die()] : Game Over !");
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_DecreaseAlivePlayers()
    {
        AlivePlayers--;
        
        if (!allPlayersDead && AlivePlayers <= 0)
        {
            Debug.Log("AllPlayersDead!!");
            allPlayersDead = true;
        }
    }
}
