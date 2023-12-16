using System;
using UnityEngine;

public class TriggerCollisionHandler : MonoBehaviour {
    
    [SerializeField] private GameObject humanDialogueEnabler;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject FButton;
    
    private OpenInventoryScript openInventory;

    private void Start() {
        openInventory = player.GetComponent<OpenInventoryScript>();
        humanDialogueEnabler.SetActive(false);
        FButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        FButton.SetActive(true);
        openInventory.CurrentlyToOpenUI = humanDialogueEnabler;
    }

    private void OnTriggerExit2D(Collider2D other) {
        FButton.SetActive(false);
        openInventory.CurrentlyToOpenUI = null;
    }
}
