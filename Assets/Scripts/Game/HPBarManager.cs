using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarManager : MonoBehaviour
{
    [SerializeField] private Slider _playerHPBar = null;
    [SerializeField] private Slider _enemyHPBar = null;

    [SerializeField] private EnemyManager _enemyManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    public void InitHPBar(int maxHealth)
    {
        _playerHPBar.maxValue = maxHealth;
        _playerHPBar.value = maxHealth;
        _enemyHPBar.maxValue = _enemyManager.GetNowEnemyMaxHealth();
    }
    
    public void UpdatePlayerHPBar(int maxHealth, int health)
    {
        _playerHPBar.maxValue = maxHealth;
        _playerHPBar.value = health;
    }

    public void UpdataEnemyHPBar()
    {
        _enemyHPBar.value = _enemyManager.GetNowEnemyHealth();
    }

    private void FixedUpdate()
    {
        //if (_enemyManager.status != EnemyManager.Status.done)
        //{
        //    UpdataEnemyHPBar();
        //}

        if (_enemyManager.status != EnemyManager.Status.done)
        {
            UpdataEnemyHPBar();
        }
        else
        {
            // 時間がねぇッ！ゴリ押しだぁッ！
            _enemyHPBar.value = 0;
            Debug.Log(string.Format("_enemyHPBar.value = {0}", _enemyHPBar.value));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
