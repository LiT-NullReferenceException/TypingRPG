using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField]private GameObject confirminationDialog;

    public void ActiveConfirminationDialog()
    {
        confirminationDialog.SetActive(true);
    }

    public void DeactiveConfirminationDialog()
    {
        confirminationDialog.SetActive(false);
    }
}
