using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyNameText;
    [SerializeField] private Image _enemySprite;
    [SerializeField] private StarController _starController;

    /// <summary>
    /// 現在ホバーによって選択されているステージの情報を表示する
    /// </summary>
    /// <param name="typingEnemy"></param>
    public void UpdateView(StageData.TypingEnemy typingEnemy)
    {
        _enemyNameText.text = typingEnemy.name;
        _enemySprite.sprite = typingEnemy.sprite;
        _starController.Init(typingEnemy.level);
    }
}
