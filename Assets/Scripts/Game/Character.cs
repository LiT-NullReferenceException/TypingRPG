using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Character : NetworkBehaviour
{
    public string characterName;
    public int maxHealth = 100;
    [Networked]public int health { get; set; }
    public int attackPower;

    public enum Status
    {
        alive = 1,
        dead = 2
    }

    /// <summary>
    /// 敵の情報をセットする
    /// </summary>
    /// <param name="enemy"></param>
    internal void SetEnemyData(StageData.TypingEnemy enemy)
    {
        maxHealth = enemy.maxHealth; // 最初のHPをセット
        attackPower = enemy.attackPower; // 攻撃力をセット
        gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", enemy.sprite.texture); // テクスチャをセット
    }

    public Status status = Status.alive;
    
    // ダメージを受けるメソッド
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void Rpc_TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(characterName + " took " + damage + " damage. Remaining Health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    // 攻撃を行うメソッド
    public virtual void Attack(Character target)
    {
        Debug.Log(characterName + " attacks " + target.characterName);
        target.Rpc_TakeDamage(attackPower);
    }

    // キャラクターが死亡した時の処理
    public virtual void Die()
    {
        Debug.Log(characterName + " has died.");
        // Destroy(gameObject);  // オブジェクトを破壊する
    }
}
