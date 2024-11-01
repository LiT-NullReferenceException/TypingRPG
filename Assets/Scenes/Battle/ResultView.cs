using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class ResultView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _resultGroup;
    [SerializeField] private CanvasGroup _gameObject;
    [SerializeField] private CanvasGroup _selectDialog;

    [ContextMenu("Play")]
    public async void DisplayResultView()
    {
        RectTransform rect = _gameObject.gameObject.GetComponent<RectTransform>();
        rect.localScale = Vector3.one * 10;
        _gameObject.alpha = 0;
        
        _resultGroup.gameObject.SetActive(true);
        _resultGroup.alpha = 0;
        await _resultGroup.DOFade(1, 1);

        _gameObject.DOFade(1, 0.1f);
        rect.DOScale(1, 0.1f);
        await rect.DOPunchPosition(Vector3.one * 10, 1, 30);

        _selectDialog.DOFade(1, 1);
    }
}
