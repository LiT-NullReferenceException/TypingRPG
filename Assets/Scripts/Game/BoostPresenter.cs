using System;
using UniRx;
using UnityEngine;

public class BoostPresenter : MonoBehaviour
{
    [SerializeField] private BoostView _boostView;
    [SerializeField] private EnemyManager _enemyManager;
    
    // EnemyManagerに、ブーストタイムかどうかを保持するReactivePropertyを作ってほしい
    // ↓ こういうやつ
    // public IReadOnlyReactiveProperty<bool> OnHogeHogeHandler => _onHogeHogeHandler;
    // private readonly ReactiveProperty<bool> _onHogeHogeHandler = new ReactiveProperty<bool>();
    
    private void Awake()
    {
        // _enemyManager.OnHogeHogeHandler
        //     .Subscribe(x =>
        //     {
        //         if (x)
        //         {
        //             _boostView.PlayBoostTimeAnimation();
        //         }
        //         else
        //         {
        //             _boostView.StopBoostTimeAnimation();
        //         }
        //     })
        //     .AddTo(this);
    }
}
