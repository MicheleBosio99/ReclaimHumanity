using System;
using System.Collections;
using UnityEngine;

public class HasAll3SpecialItemsBehaviour : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject DrIDKHuman;
    [SerializeField] private ItemsSO finalRecipeItem;
    [SerializeField] private InventoryItemsSO inventoryItemSO;
    [SerializeField] private GameObject unlockUI;
    
    private OpenInventoryScript openInventoryScript;
    private PlayerMovement playerMovement;

    private void Awake() {
        openInventoryScript = player.GetComponent<OpenInventoryScript>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Start() {
        if (GameManager.hasAll3SpecialItems.hasAll3Items && !GameManager.hasAll3SpecialItems.hasAlreadyTalkedToIDK) {
            StartCoroutine(TalkToIDK());
        }
    }

    private IEnumerator TalkToIDK() {
        
        unlockUI.GetComponent<VisualizeUnlocked>().StartShowUnlockedRecipeMessage("You retrieved all 3 special items! Let's go see Dr.IDK!");
        
        UnBlockPlayerInput(true);
        
        yield return playerMovement.WalkPlayer(new Vector2(0.5f, -1.0f));
        yield return playerMovement.WalkPlayer(new Vector2(-16.35f, -1.0f));
        yield return playerMovement.WalkPlayer(new Vector2(-16.35f, 11.0f));
        yield return new WaitForSeconds(0.5f);
        
        UnBlockPlayerInput(false);
        
        inventoryItemSO.EmptySpecialItemSlots();
        if (!inventoryItemSO.AddOrdinaryItemToInventory(finalRecipeItem.ToInventoryItem(1))) {
            inventoryItemSO.RemoveLastOrdinaryItem();
            inventoryItemSO.AddOrdinaryItemToInventory(finalRecipeItem.ToInventoryItem(1));
        }
        
        
        DrIDKHuman.GetComponent<InteractionHumanHandler>().HumanID = "AAA_Dr-IDK_Lab-3SpecialItems";
        
        openInventoryScript.OpenCloseUIFunc(true);
    }

    private void UnBlockPlayerInput(bool value) {
        openInventoryScript.InventoryIsOpen = value;
        openInventoryScript.isActive = value;
        openInventoryScript.Finished = !value;
    }
}
