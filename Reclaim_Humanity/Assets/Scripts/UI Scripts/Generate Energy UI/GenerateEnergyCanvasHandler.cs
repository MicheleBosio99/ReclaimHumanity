using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GenerateEnergyCanvasHandler : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private PlayerMovement movement;
    
    private void Start() {
        movement = player.GetComponent<PlayerMovement>();
    }

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
        if(movement != null) { movement.CurrentSpeed = 0.0f; }
    }

    private void OnDisable() { if (movement != null) { movement.CurrentSpeed = movement.NormalSpeed; } }

    public void OnCloseButtonClick() { player.GetComponent<OpenInventoryScript>().OpenCloseUIFunc(true); }
}
