using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BoostView : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private TextMeshProUGUI _boostText;

    private Tweener _tweener;

    private bool isFirst = true;

    private void Update()
    {
        if (_enemyManager.isBoosting && isFirst)
        {
            isFirst = false;
            PlayBoostTimeAnimation();
            _boostText.gameObject.SetActive(true);
        }

        if (!_enemyManager.isBoosting && !isFirst)
        {
            isFirst = true;
            StopBoostTimeAnimation();
            _boostText.gameObject.SetActive(false);
        }
    }

    public void PlayBoostTimeAnimation()
    {
        _tweener = _boostText.DOFade(0, 0.3f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopBoostTimeAnimation()
    {
        _tweener.Kill();
        _boostText.DOFade(1, 0.1f);
    }
}
