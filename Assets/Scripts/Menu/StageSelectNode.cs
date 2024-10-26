using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelectNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _hoverMoveDistance = 30;
    [SerializeField] private float _hoverMoveDuration = 0.3f;
    [SerializeField] private Image _nodeButton;
    [SerializeField] private TextMeshProUGUI _enemyNameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Sprite _normalButtonSprite;
    [SerializeField] private Sprite _hoveredButtonSprite;

    private RectTransform _nodeButtonRectTransform;
    private StageData.TypingEnemy _enemy;
    private StageHelper _stageHelper;

    /// <summary>
    ///  初期化する
    /// </summary>
    /// <param name="enemyName">敵の名前</param>
    /// <param name="level">難易度</param>
    public void Init(StageData.TypingEnemy enemy, StageHelper stageHelper)
    {
        _enemyNameText.text = enemy.name;
        _levelText.text = enemy.level.ToString();
        _enemy = enemy;
        _stageHelper = stageHelper;

        _nodeButtonRectTransform = _nodeButton.gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _nodeButtonRectTransform.DOAnchorPosX(_hoverMoveDistance, _hoverMoveDuration);
        _nodeButton.sprite = _hoveredButtonSprite;
        _stageHelper.UpdateView(_enemy);
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _nodeButtonRectTransform.DOAnchorPosX(-_hoverMoveDistance, _hoverMoveDuration);
        _nodeButton.sprite = _normalButtonSprite;
    }
}
