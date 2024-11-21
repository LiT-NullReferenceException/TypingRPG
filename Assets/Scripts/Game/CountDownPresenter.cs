using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownPresenter : MonoBehaviour
{
    [SerializeField] private CountDownText _countDownText;
    [SerializeField] private TimeManager _timeManager;
    
    // TimeManager、残り時間を保持するReactivePropertyを作ってほしい
    // ↓ こういうやつ
    // public IReadOnlyReactiveProperty<float> OnHogeHogeHandler => _onHogeHogeHandler;
    // private readonly ReactiveProperty<float> _onHogeHogeHandler = new ReactiveProperty<float>();

    private void Awake()
    {
        // _timeManager.OnHogeHogeHandler
        //     .Subscribe(x =>
        //     {
        //         _countDownText.UpdateText(x);
        //     })
        //     .AddTo(this);
    }
}
