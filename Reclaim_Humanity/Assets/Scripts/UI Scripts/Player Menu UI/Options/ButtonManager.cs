using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour {

    [SerializeField] private GameObject YouSureQuitView;
    [SerializeField] private Player player;

    private void Start() { YouSureQuitView.SetActive(false); }

    public void OnSaveClick() { player.SaveGame(); }

    public void OnLoadClick() { player.LoadGame(); }

    public void OnQuitGameClick() { YouSureQuitView.SetActive(true); }

    public void OnGoBackClick() { YouSureQuitView.SetActive(false); }

    public void OnReallyQuitGameClick() { Application.Quit(); }

    

    
}
