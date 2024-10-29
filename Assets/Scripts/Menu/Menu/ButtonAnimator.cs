using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scalingRatio = 1.5f;
    [SerializeField] private float _scalingDuration = 0.5f;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        transform.DOScale(_scalingRatio, _scalingDuration);
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        transform.DOScale(1, _scalingDuration);
    }
}
