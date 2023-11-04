using System.Collections.Generic;
using UnityEngine;

public class HandleItemsInInventory : MonoBehaviour {
    
    [SerializeField] private List<GameObject> ordinaryInventorySlotsUI;
    [SerializeField] private List<GameObject> specialInventorySlotsUI;
    [SerializeField] private GameObject player;

    [SerializeField] private InventoryItemsSO inventorySlotsSO;
    
    private void Start() {
        // UpdateOrdinarySlots(); UpdateSpecialSlots();
    }

    public void AddNewOrdinaryItemToInventory(InventoryItem item) {
        if(inventorySlotsSO.AddOrdinaryItemToInventory(item)) { UpdateOrdinarySlots(); }
    }

    public void AddNewSpecialItemToInventory(InventoryItem item) {
        if(inventorySlotsSO.AddSpecialItemToInventory(item)) { UpdateSpecialSlots(); }
    }

    private void UpdateOrdinarySlots() {
        for (int i = 0; i < inventorySlotsSO.OrdinaryItemsInInventory.Count; i++) {
            ordinaryInventorySlotsUI[i].GetComponent<InventoryItemSlot>()
                .FillSlotWithItem(inventorySlotsSO.OrdinaryItemsInInventory[i]);
        }
    }
    
    private void UpdateSpecialSlots() {
        for (int i = 0; i < inventorySlotsSO.SpecialItemsInInventory.Count; i++) {
            ordinaryInventorySlotsUI[i].GetComponent<InventoryItemSlot>()
                .FillSlotWithItem(inventorySlotsSO.SpecialItemsInInventory[i]);
        }
    }

    //public void OnButtonClick() { AddNewOrdinaryItemToInventory(new InventoryItem("id0", null, 10)); }
}
