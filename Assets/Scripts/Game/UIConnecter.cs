using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI とGameManager を繋ぐやつ
public class UIConnecter : MonoBehaviour
{
    // クイズ（お題、入力する文字）が更新されたら呼ばれるデリゲート
    // ヒミツマスを裏返したときに実行される
    //public delegate UniTask SecretCellPerformanceExecutedDelegate();
    //public event SecretCellPerformanceExecutedDelegate OnSecretCellPerformanceExecuted;
    //async public UniTask SecretCellPerformance()
    //{
    //    Debug.Log("<b><color=#ef476f>【Board - FlipSecretCell】ヒミツマスを裏返したときの演出</color></b>");
    //    if (OnSecretCellPerformanceExecuted != null) { await OnSecretCellPerformanceExecuted(); }
    //}

    // コンボが減った時に呼ばれる
    public delegate void WhenCharComboDecreasedExecutedDelegate();
    public event WhenCharComboDecreasedExecutedDelegate WhenCharComboDecreasedExecuted;
    public void WhenCharComboDecreased()
    {
        Debug.Log("<b><color=#ff0000>【UIConnecter - WhenCharComboDecreased】コンボが減った</color></b>");
        if (WhenCharComboDecreasedExecuted != null) { WhenCharComboDecreasedExecuted(); }
    }

    // コンボが増えた時に呼ばれる
    public delegate void WhenCharComboIncreasedExecutedDelegate();
    public event WhenCharComboDecreasedExecutedDelegate WhenCharComboIncreasedExecuted;
    public void WhenCharComboIncreased()
    {
        Debug.Log("<b><color=#ff8700>【UIConnecter - WhenCharComboIncreased】コンボが増えた</color></b>");
        if (WhenCharComboIncreasedExecuted != null) { WhenCharComboIncreasedExecuted(); }
    }

    // プレイヤーが敵に攻撃をするときに呼ばれる
    public delegate void WhenPlayerAttackToEnemyExecutedDelegate();
    public event WhenPlayerAttackToEnemyExecutedDelegate WhenPlayerAttackToEnemyExecuted;
    public void WhenPlayerAttackToEnemy()
    {
        Debug.Log("<b><color=#ffd300>【UIConnecter - WhenPlayerAttackToEnemy】攻撃 : プレイヤー => 敵</color></b>");
        if (WhenPlayerAttackToEnemyExecuted != null) { WhenPlayerAttackToEnemyExecuted(); }
    }

    // 敵がプレイヤーに攻撃をするときに呼ばれる
    public delegate void WhenEnemyAttackToPlayerExecutedDelegate();
    public event WhenEnemyAttackToPlayerExecutedDelegate WhenEnemyAttackToPlayerExecuted;
    public void WhenEnemyAttackToPlayer()
    {
        Debug.Log("<b><color=#deff0a>【UIConnecter - WhenEnemyAttackToPlayer】攻撃 : 敵 => プレイヤー</color></b>");
        if (WhenEnemyAttackToPlayerExecuted != null) { WhenEnemyAttackToPlayerExecuted(); }
    }

    // クイズ（お題）が変更されたときに呼ばれる
    public delegate void WhenRefreshQuizExecutedDelegate();
    public event WhenRefreshQuizExecutedDelegate WhenRefreshQuizExecuted;
    public void WhenRefreshQuiz()
    {
        Debug.Log("<b><color=#a1ff0a>【UIConnecter - WhenRefreshQuiz】クイズが更新されました</color></b>");
        if (WhenRefreshQuizExecuted != null) { WhenRefreshQuizExecuted(); }
    }


}
