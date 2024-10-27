using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI ��GameManager ���q�����
public class UIConnecter : MonoBehaviour
{
    // �N�C�Y�i����A���͂��镶���j���X�V���ꂽ��Ă΂��f���Q�[�g
    // �q�~�c�}�X�𗠕Ԃ����Ƃ��Ɏ��s�����
    //public delegate UniTask SecretCellPerformanceExecutedDelegate();
    //public event SecretCellPerformanceExecutedDelegate OnSecretCellPerformanceExecuted;
    //async public UniTask SecretCellPerformance()
    //{
    //    Debug.Log("<b><color=#ef476f>�yBoard - FlipSecretCell�z�q�~�c�}�X�𗠕Ԃ����Ƃ��̉��o</color></b>");
    //    if (OnSecretCellPerformanceExecuted != null) { await OnSecretCellPerformanceExecuted(); }
    //}

    // �R���{�����������ɌĂ΂��
    public delegate void WhenCharComboDecreasedExecutedDelegate();
    public event WhenCharComboDecreasedExecutedDelegate WhenCharComboDecreasedExecuted;
    public void WhenCharComboDecreased()
    {
        Debug.Log("<b><color=#ff0000>�yUIConnecter - WhenCharComboDecreased�z�R���{��������</color></b>");
        if (WhenCharComboDecreasedExecuted != null) { WhenCharComboDecreasedExecuted(); }
    }

    // �R���{�����������ɌĂ΂��
    public delegate void WhenCharComboIncreasedExecutedDelegate();
    public event WhenCharComboDecreasedExecutedDelegate WhenCharComboIncreasedExecuted;
    public void WhenCharComboIncreased()
    {
        Debug.Log("<b><color=#ff8700>�yUIConnecter - WhenCharComboIncreased�z�R���{��������</color></b>");
        if (WhenCharComboIncreasedExecuted != null) { WhenCharComboIncreasedExecuted(); }
    }

    // �v���C���[���G�ɍU��������Ƃ��ɌĂ΂��
    public delegate void WhenPlayerAttackToEnemyExecutedDelegate();
    public event WhenPlayerAttackToEnemyExecutedDelegate WhenPlayerAttackToEnemyExecuted;
    public void WhenPlayerAttackToEnemy()
    {
        Debug.Log("<b><color=#ffd300>�yUIConnecter - WhenPlayerAttackToEnemy�z�U�� : �v���C���[ => �G</color></b>");
        if (WhenPlayerAttackToEnemyExecuted != null) { WhenPlayerAttackToEnemyExecuted(); }
    }

    // �G���v���C���[�ɍU��������Ƃ��ɌĂ΂��
    public delegate void WhenEnemyAttackToPlayerExecutedDelegate();
    public event WhenEnemyAttackToPlayerExecutedDelegate WhenEnemyAttackToPlayerExecuted;
    public void WhenEnemyAttackToPlayer()
    {
        Debug.Log("<b><color=#deff0a>�yUIConnecter - WhenEnemyAttackToPlayer�z�U�� : �G => �v���C���[</color></b>");
        if (WhenEnemyAttackToPlayerExecuted != null) { WhenEnemyAttackToPlayerExecuted(); }
    }

    // �N�C�Y�i����j���ύX���ꂽ�Ƃ��ɌĂ΂��
    public delegate void WhenRefreshQuizExecutedDelegate();
    public event WhenRefreshQuizExecutedDelegate WhenRefreshQuizExecuted;
    public void WhenRefreshQuiz()
    {
        Debug.Log("<b><color=#a1ff0a>�yUIConnecter - WhenRefreshQuiz�z�N�C�Y���X�V����܂���</color></b>");
        if (WhenRefreshQuizExecuted != null) { WhenRefreshQuizExecuted(); }
    }


}
