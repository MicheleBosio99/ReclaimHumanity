using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseUITrigger : MonoBehaviour {
    private SpriteRenderer FButtonRenderer;
    
    [SerializeField] private GameObject FButton;
    [SerializeField] private GameObject ToOpenUI;
    [SerializeField] private GameObject player;
    private OpenInventoryScript _openInventoryScript;
    
    private void Start() {
        FButtonRenderer = FButton.GetComponent<SpriteRenderer>();
        FButtonRenderer.enabled = false;
        ToOpenUI.SetActive(false);
        _openInventoryScript = player.GetComponent<OpenInventoryScript>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        FButtonRenderer.enabled = true;
        _openInventoryScript.CurrentlyToOpenUI = ToOpenUI;
    }

    private void OnTriggerExit2D(Collider2D other) {
        FButtonRenderer.enabled = false;
        _openInventoryScript.CurrentlyToOpenUI = null;
    }
}
