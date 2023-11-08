using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGenEnergySlots : MonoBehaviour
{
    [SerializeField] private LabEnergySO labEnergySO;
    [SerializeField] private InventoryItemsSO inventorySO;
    private List<InventoryItem> inventorySOItems;

    [SerializeField] private List<GameObject> inventorySlotsUI;
    [SerializeField] private List<GameObject> chosenItemSlotsUI;

    private TextsHandler textsHandler;
    private HandleItemInSlot handleItemInSlot;

    private void OnEnable() {
        textsHandler = gameObject.GetComponent<TextsHandler>();
        handleItemInSlot = gameObject.GetComponent<HandleItemInSlot>();

        inventorySOItems = inventorySO.OrdinaryItemsInInventory;

        for (var i = 0; i < inventorySOItems.Count; i++) {
            inventorySlotsUI[i].GetComponent<HandleItemInSlotGE>().FillSlotWithItem(inventorySOItems[i]);
        }
    }
    
    
}
