using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIconAnimator : MonoBehaviour
{
    [SerializeField] private Image _loadingIcon;

    private void Awake()
    {
        _loadingIcon.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);
    }
}
