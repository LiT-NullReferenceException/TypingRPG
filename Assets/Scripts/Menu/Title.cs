using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] private Image _pressEnterToStartText;
    [SerializeField] private float _fadeAmount = 0.3f;
    [SerializeField] private float _fadeDuration = 1;
    
    private void Awake()
    {
        _pressEnterToStartText.DOFade(_fadeAmount, _fadeDuration)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            // シーン遷移
            
        }
    }
}
