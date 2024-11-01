using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public enum Status
    {
        Idle = 0,
        Playing = 1,
        TimeUp = 2,
    }
    public Status status = Status.Idle;

    public float time = 180.0f; // ゲーム時間
    public float timer = 5.0f; // 残りのゲーム時間
    
    // Start is called before the first frame update
    void Start()
    {
        status = Status.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (status != Status.Playing) { return; }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            status = Status.TimeUp;
            Debug.Log("[TimeManager - Update()] : Time Up !");
        }
    }
}
