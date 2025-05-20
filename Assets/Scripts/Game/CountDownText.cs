using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownText : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private TextMeshProUGUI _timeText;
    
    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(_timeManager.timer / 60f);
        int seconds = Mathf.FloorToInt(_timeManager.timer % 60f);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// <summary>
    /// 表示する秒数を更新する
    /// </summary>
    /// <param name="time">秒数</param>
    public void UpdateText(float time)
    {
        _timeText.text = time.ToString("00");
    }
}
