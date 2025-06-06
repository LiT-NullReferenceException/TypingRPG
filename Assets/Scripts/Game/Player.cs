using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class Player : Character
{
    public static readonly List<Player> CurrentPlayers = new List<Player>();
    [Networked] public RoomPlayer RoomUser { get; set; }
    public String name;
    
    public static bool allPlayersDead = false; 
    
    public override void Spawned()
    {
        base.Spawned();
        health = maxHealth;
        //attackPower = 20;
        
        CurrentPlayers.Add(this);
        Debug.Log("Player " + name + " spawned");
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        base.Despawned(runner, hasState);
        CurrentPlayers.Remove(this);
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
        Rpc_CheckAlivePlayers();

        Debug.Log("[Player - Die()] : Game Over !");
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_CheckAlivePlayers()
    {
        if (!allPlayersDead && CurrentPlayers.Count(x => x.status == Status.alive) <= 0)
        {
            Debug.Log("AllPlayersDead!!");
            allPlayersDead = true;
        }
    }
}
