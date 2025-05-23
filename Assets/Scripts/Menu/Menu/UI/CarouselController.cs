using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CarouselController : MonoBehaviour
{
    [SerializeField] private RectTransform contentTransform;
    [SerializeField] private float slideDuration = 0.4f;
    [SerializeField] private float slideWidth = 1000f; // 画像＋Spacingを加味
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private int totalSlides = 7;

    private int currentIndex = 0;
    private bool isAnimating = false;

    void Start()
    {
        UpdateButtonInteractable();
        nextButton.onClick.AddListener(SlideNext);
        backButton.onClick.AddListener(SlideBack);
    }

    private void SlideNext()
    {
        if (isAnimating || currentIndex >= totalSlides - 1) return;

        currentIndex++;
        AnimateSlide();
    }

    private void SlideBack()
    {
        if (isAnimating || currentIndex <= 0) return;

        currentIndex--;
        AnimateSlide();
    }

    private void AnimateSlide()
    {
        isAnimating = true;
        float targetX = -slideWidth * currentIndex;

        contentTransform.DOAnchorPosX(targetX, slideDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                isAnimating = false;
                UpdateButtonInteractable();
            });
    }

    private void UpdateButtonInteractable()
    {
        backButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < totalSlides - 1;
    }
}