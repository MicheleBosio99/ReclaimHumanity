using System;
using UnityEngine;

public class TriggerCollisionHandlerIDK : MonoBehaviour {
    
    [SerializeField] private GameObject humanDialogueEnabler;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject FButton;
    [SerializeField] private InventoryItemsSO inventoryItemSO;
    [SerializeField] private GameObject DrIDKHuman;
    [SerializeField] private GameObject EndGameUI;
    
    private OpenInventoryScript openInventory;

    private void Start() {
        openInventory = player.GetComponent<OpenInventoryScript>();
        humanDialogueEnabler.SetActive(false);
        FButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (inventoryItemSO.SearchOrdinaryItemByID("S_FinalPotion").ItemID != "") {
            DrIDKHuman.GetComponent<InteractionHumanHandler>().ChangeHumanID("AAA_Dr-IDK_Lab-EndGame");
        }

        FButton.SetActive(true);
        openInventory.CurrentlyToOpenUI = humanDialogueEnabler;
    }

    private void OnTriggerExit2D(Collider2D other) {
        FButton.SetActive(false);
        openInventory.CurrentlyToOpenUI = null;
        
        if (inventoryItemSO.SearchOrdinaryItemByID("S_FinalPotion").ItemID != "") {
            inventoryItemSO.RemoveItemByID("S_FinalPotion");
            if (EndGameUI != null) { EndGameUI.SetActive(true); }
        }
    }
}
