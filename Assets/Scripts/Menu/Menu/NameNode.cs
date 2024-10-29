using TMPro;
using UnityEngine;

public class NameNode : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    public void SetName(string name)
    {
        _nameText.text = name;
    }
}
