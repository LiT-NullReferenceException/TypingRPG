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
    public void UpdateView(StageData.Stage stage)
    {
        _enemyNameText.text = stage.name;
        _enemySprite.sprite = stage.mainEnemy.sprite;
        _starController.Init(stage.level);
    }
}
