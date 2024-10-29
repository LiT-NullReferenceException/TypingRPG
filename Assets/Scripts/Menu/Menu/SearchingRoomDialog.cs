using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SearchingRoomDialog : MonoBehaviour
{
    [SerializeField] private Image _loadingIcon;
    [SerializeField] private Button _cancelButton;

    private void Awake()
    {
        _cancelButton.onClick.AddListener(() =>
        {
            
        });

        _loadingIcon.gameObject.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart);
    }
}
