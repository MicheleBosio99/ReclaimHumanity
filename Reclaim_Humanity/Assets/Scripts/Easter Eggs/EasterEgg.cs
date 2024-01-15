using UnityEngine;

public class EasterEgg : MonoBehaviour {
    
    [SerializeField] private GameObject unlockUI;
    
    [SerializeField] private GameObject PowerUpsManager;
    private bool doneEnergy;
    private bool doneItem;
    
    private void Update() {
        CheckEnergyEasterEgg();
        CheckItemEasterEgg();
        if (doneItem && doneEnergy) { Destroy(gameObject); }
    }
    
    private const string desiredEnergyCombination = "energy";
    private string currentEnergyInput = "";

    private void CheckEnergyEasterEgg() {
        if (!Input.anyKeyDown) return;
            
        if (!desiredEnergyCombination.Contains(Input.inputString)) { currentEnergyInput = ""; return; }
        currentEnergyInput += Input.inputString;

        if (!currentEnergyInput.Equals(desiredEnergyCombination, System.StringComparison.OrdinalIgnoreCase)) return;
        
        ActivateEnergyEasterEgg();
        currentEnergyInput = "";
    }

    private void ActivateEnergyEasterEgg() {
        unlockUI.GetComponent<VisualizeUnlocked>().StartShowUnlockedRecipeMessage("Activated Energy Easter Egg.\nYou were given 50 energy.");
        PowerUpsManager.GetComponent<PowerUpsManager>().UpdatePowerUps();
        GameManager.energyInLab += 50;
    }
    
    
    [SerializeField] private InventoryItemsSO inventoryItemSO;
    [SerializeField] private ItemsSO casualItem;
    
    private const string desiredItemCombination = "items";
    private string currentItemInput = "";

    private void CheckItemEasterEgg() {
        if (!Input.anyKeyDown) return;
            
        if (!desiredItemCombination.Contains(Input.inputString)) { currentItemInput = ""; return; }
        currentItemInput += Input.inputString;

        if (!currentItemInput.Equals(desiredItemCombination, System.StringComparison.OrdinalIgnoreCase)) return;
        
        ActivateItemEasterEgg();
        currentItemInput = "";
    }

    private void ActivateItemEasterEgg() {
        unlockUI.GetComponent<VisualizeUnlocked>().StartShowUnlockedRecipeMessage("Activated Item Easter Egg.\nYou were given 4 Final Potions.");
        GameManager.hasAll3SpecialItems.hasAll3Items = true;
        inventoryItemSO.AddOrdinaryItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddSpecialItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddSpecialItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddSpecialItemToInventory(casualItem.ToInventoryItem(1));
    }
}