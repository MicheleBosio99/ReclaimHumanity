using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour {
    
    [SerializeField] private GameObject Background;

    private void Awake() { Background.SetActive(false); }

    public void OnOpenClick() { Background.SetActive(true); }
    
    public void OnCloseClick() { Background.SetActive(false); }
    
}
