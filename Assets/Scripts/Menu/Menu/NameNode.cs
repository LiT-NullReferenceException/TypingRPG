using TMPro;
using UnityEngine;

public class NameNode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    public void SetName(string name)
    {
        Debug.Log("nameSet!");
        _nameText.text = name;
    }
}
