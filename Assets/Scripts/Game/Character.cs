using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth = 100;
    public int health;
    public int attackPower;

    public enum Status
    {
        alive = 1,
        dead = 2
    }

    public Status status = Status.alive;

    // ダメージを受けるメソッド
    public void TakeDamage(int damage)
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
        target.TakeDamage(attackPower);
    }

    // キャラクターが死亡した時の処理
    public virtual void Die()
    {
        Debug.Log(characterName + " has died.");
        // Destroy(gameObject);  // オブジェクトを破壊する
    }
}
