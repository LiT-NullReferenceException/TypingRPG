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
        _timeText.text = _timeManager.timer.ToString("00");
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
