using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TittleUI : MonoBehaviour
{
    //[SerializeField] private AudioManager audioManager;
    void Awake()
    {
        UIScreen.Focus(GetComponent<UIScreen>());
    }

    void Start()
    {
        // タイトル画面のBGMを再生
        AudioManager.instance_AudioManager.PlayBGM(0);
    }
}