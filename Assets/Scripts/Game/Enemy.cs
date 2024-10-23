using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    void Start()
    {
        //characterName = "Enemy";
        health = maxHealth;
        //attackPower = 10;
    }

    // 敵の特殊な攻撃などをここに追加可能
    public override void Attack(Character target)
    {
        base.Attack(target);
        // 追加効果があればここで処理する
    }

    private EnemyManager enemyManager = null;
    public EnemyManager SetEnemyManager { set => enemyManager = value; }

    public override void Die()
    {
        enemyManager.DieEnemy();
    }
}
