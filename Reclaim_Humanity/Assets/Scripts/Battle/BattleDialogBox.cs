using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }
}
