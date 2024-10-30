using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TitleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI _text;
    private string _content;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _content = _text.text;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _text.fontStyle = FontStyles.Underline;
        _text.text = $"※{_text.text}※";
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _text.fontStyle = FontStyles.Normal;
        _text.text = _content;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // いっちーへ
        // クリックされた時の処理を、ここに書く
        
        
        
        
        
    }
}
