using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleUI : MonoBehaviour
{
    void Awake()
    {
        UIScreen.Focus(GetComponent<UIScreen>());
    }
}