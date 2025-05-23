using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

public class Player : Character
{
    [Networked] public RoomPlayer RoomUser { get; set; }
    public static readonly List<Player> PlayerList = new List<Player>();
    public String name;
    
    public static bool allPlayersDead = false; 
    
    public override void Spawned()
    {
        base.Spawned();
        //characterName = "Player";
        health = maxHealth;
        //attackPower = 20;
        
        PlayerList.Add(this);
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

        Debug.Log("[Player - Die()] : Game Over !");

        if (!allPlayersDead && PlayerList.All(x => x.health <= 0))
        {
            allPlayersDead = true;
        }
    }
}
