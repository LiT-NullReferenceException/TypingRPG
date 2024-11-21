using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _text;
    private string _content;

    private void Awake()
    {
        _content = _text.text;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _text.fontStyle = FontStyles.Underline;
        _text.text = $"※ {_text.text} ※";
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _text.fontStyle = FontStyles.Normal;
        _text.text = _content;
    }
}
