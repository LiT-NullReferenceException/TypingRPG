using TMPro;
using UnityEngine;

public class ThemeTextView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _japaneseText;
    [SerializeField] private TextMeshProUGUI _romanText;

    public void SetText(Quiz quiz)
    {
        _japaneseText.text = quiz.japanese;
        _romanText.text = quiz.roman;
    }

    public void UpdateTextState()
    {
        
    }
}
