using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public void OnButtonClick() {
        PlayerPrefs.DeleteAll();
        GameManager.NewGame();
    }
}
