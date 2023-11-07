using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour {

    [SerializeField] private GameObject YouSureQuitView;

    private void Start() { YouSureQuitView.SetActive(false); }

    public void OnSaveClick() { SaveGame(); }

    public void OnQuitGameClick() { YouSureQuitView.SetActive(true); }

    public void OnGoBackClick() { YouSureQuitView.SetActive(false); }

    public void OnReallyQuitGameClick() { Application.Quit(); }

    private void SaveGame() { Debug.Log("Game saved successfully;\n"); } // TODO
}
