using System;
using Fusion;
using UnityEngine;
using DG.Tweening;

public class ResultView : NetworkBehaviour
{
    [SerializeField] private CanvasGroup _resultGroup;
    [SerializeField] private CanvasGroup _gameObject;
    [SerializeField] private CanvasGroup _selectDialog;

    [ContextMenu("Play")]
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_DisplayResultView()
    {
        RectTransform rect = _gameObject.gameObject.GetComponent<RectTransform>();
        rect.localScale = Vector3.one * 10;
        _gameObject.alpha = 0;

        _resultGroup.gameObject.SetActive(true);
        _resultGroup.alpha = 0;

        // _resultGroup のフェードイン
        _resultGroup.DOFade(1, 1).OnComplete(() =>
        {
            // _gameObject のフェードインとスケールダウン
            _gameObject.DOFade(1, 0.1f);
            rect.DOScale(1, 0.1f).OnComplete(() =>
            {
                // rect のパンチアニメーション
                rect.DOPunchPosition(Vector3.one * 10, 1, 30).OnComplete(() =>
                {
                    // _selectDialog のフェードイン
                    _selectDialog.DOFade(1, 1);
                });
            });
        });
    }
}