using System;
using UnityEngine;

public class ResultViewController : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private EnemyManager _enemyManager;

    [SerializeField] private ResultView _gameOverPanel;
    [SerializeField] private ResultView _gameClearPanel;

    private bool isFirst = true;
    
    [SerializeField] private AudioManager audioManager = null;

    private void Start()
    {
        // AudioManager を参照する
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (_timeManager.timer <= 0 && isFirst)
        {
            isFirst = false;
            audioManager.StopBGM();
            audioManager.PlayBGM(4);
            _gameOverPanel.Rpc_DisplayResultView();
        }

        if (_enemyManager.GetNowEnemyHealth() == 0 && isFirst)
        {
            isFirst = false;
            audioManager.StopBGM();
            audioManager.PlayBGM(3);
            _gameClearPanel.Rpc_DisplayResultView();
        }
    }
}
