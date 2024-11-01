using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Player : Character
{
    [Networked] public RoomPlayer RoomUser { get; set; }
    public String name;
    
    void Spawned()
    {
        //characterName = "Player";
        health = maxHealth;
        //attackPower = 20;
        
        Debug.Log("Player " + name + " spawned");
        
        name = RoomUser.Username.Value; // こんな感じで取得できる
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
    }
}
