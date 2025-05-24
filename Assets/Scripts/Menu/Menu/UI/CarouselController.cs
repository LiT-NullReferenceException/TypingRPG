using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UI;

public class CarouselController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("RectTransform of the Content panel that holds your slides.")]
    [SerializeField] private RectTransform contentTransform;
    [Tooltip("Next and Back buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;

    [Header("Animation Settings")]
    [Tooltip("Time in seconds for one slide animation.")]
    [SerializeField] private float slideDuration = 0.4f;

    private RectTransform[] slides;
    private int currentIndex = 0;
    private bool isAnimating = false;

    void Start()
    {
        // 1) レイアウトが完全に更新されるまで待機／強制再構築
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);

        // 2) 子スライドを配列に取得
        int count = contentTransform.childCount;
        slides = new RectTransform[count];
        for (int i = 0; i < count; i++)
        {
            slides[i] = contentTransform.GetChild(i) as RectTransform;
        }

        // 3) ボタンコールバック登録
        nextButton.onClick.AddListener(SlideNext);
        backButton.onClick.AddListener(SlideBack);

        // 4) 初期表示を最初のスライドに合わせる
        MoveToCurrentSlideInstant();
        UpdateButtonInteractable();
    }

    private void SlideNext()
    {
        if (isAnimating || currentIndex >= slides.Length - 1) return;
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

        // 子スライドのローカル位置を使ったターゲット X
        float targetX = -slides[currentIndex].localPosition.x;

        // DOTween で滑らかにスライド
        contentTransform
            .DOAnchorPosX(targetX, slideDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                isAnimating = false;
                UpdateButtonInteractable();
            });
    }

    private void MoveToCurrentSlideInstant()
    {
        // 即時に位置合わせ（Start 時やリセット時）
        float targetX = -slides[currentIndex].localPosition.x;
        contentTransform.anchoredPosition = new Vector2(targetX, contentTransform.anchoredPosition.y);
    }

    private void UpdateButtonInteractable()
    {
        backButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < slides.Length - 1;
    }
}
