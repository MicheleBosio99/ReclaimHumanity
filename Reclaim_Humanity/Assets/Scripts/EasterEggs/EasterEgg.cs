using UnityEngine;

public class EasterEgg : MonoBehaviour {
    
    private bool doneEnergy;
    private bool doneItem;
    
    private void Update() {
        CheckEnergyEasterEgg();
        CheckItemEasterEgg();
        if (doneItem && doneEnergy) { Destroy(gameObject); }
    }
    
    private const string desiredEnergyCombination = "bosio";
    private string currentEnergyInput = "";

    private void CheckEnergyEasterEgg() {
        if (!Input.anyKeyDown) return;
            
        if (!desiredEnergyCombination.Contains(Input.inputString)) { currentEnergyInput = ""; return; }
        currentEnergyInput += Input.inputString;

        if (!currentEnergyInput.Equals(desiredEnergyCombination, System.StringComparison.OrdinalIgnoreCase)) return;
        
        ActivateEnergyEasterEgg();
        currentEnergyInput = "";
    }
    
    private void ActivateEnergyEasterEgg() { Debug.Log("Activated Energy Easter Egg"); GameManager.energyInLab += 50; }
    
    
    [SerializeField] private InventoryItemsSO inventoryItemSO;
    [SerializeField] private ItemsSO casualItem;
    
    private const string desiredItemCombination = "item";
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
        Debug.Log("Activated Item Easter Egg");
        GameManager.hasAll3SpecialItems.hasAll3Items = true;
        inventoryItemSO.AddOrdinaryItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddOrdinaryItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddSpecialItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddSpecialItemToInventory(casualItem.ToInventoryItem(1));
        inventoryItemSO.AddSpecialItemToInventory(casualItem.ToInventoryItem(1));
    }
}