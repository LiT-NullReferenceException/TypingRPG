using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

// EnemyManager は全てのプレイヤーで共通 → シングルトンにする

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int _enemyIndex = 0;
    [SerializeField] private Enemy[] _enemies = null;

    public enum Status
    {
        inprogress = 0,
        done = 1,
    }

    public Status status = Status.inprogress;


    public void TakeDamage(int damage)
    {
        _enemies[_enemyIndex].TakeDamage(damage);
    }

    public int GetAttackPower()
    {
        return _enemies[_enemyIndex].attackPower;
    }

    // 全てのモンスターが倒されたら true を返す
    public void DieEnemy()
    {
        _enemyIndex++;

        // 最後のモンスターが倒れたか調べる
        if (_enemyIndex == _enemies.Length)
        {
            Debug.Log("[EnemyManager - DieEnemy()] : ステージ攻略！");

            status = Status.done;
        }
    }

    public int GetNowEnemyMaxHealth()
    {
        return _enemies[_enemyIndex].maxHealth;
    }

    public int GetNowEnemyHealth()
    {
        return _enemies[_enemyIndex].health;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i].SetEnemyManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
