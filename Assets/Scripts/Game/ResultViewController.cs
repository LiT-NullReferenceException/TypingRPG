using System;
using UnityEngine;

public class ResultViewController : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private ResultView _gameOverPanel;
    [SerializeField] private ResultView _gameClearPanel;

    private bool isFirst = true;
    
    private void Update()
    { ;
        if (_timeManager.timer <= 0 && isFirst)
        {
            isFirst = false;
            _gameOverPanel.DisplayResultView();
        }

        if (_enemyManager.GetNowEnemyHealth() == 0 && isFirst)
        {
            isFirst = false;
            _gameClearPanel.DisplayResultView();
        }
    }
}
