using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class HandleItemsInInventory : MonoBehaviour {
    
    [SerializeField] private List<GameObject> ordinaryInventorySlotsUI;
    [SerializeField] private List<GameObject> specialInventorySlotsUI;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject DescriptionText;
    [SerializeField] private GameObject EnergyText;

    [SerializeField] private InventoryItemsSO inventorySlotsSO;

    public GameObject CurrentSelectedSlot { get; set; }

    public List<GameObject> OrdinaryInventorySlotsUI {
        get => ordinaryInventorySlotsUI;
        set => ordinaryInventorySlotsUI = value;
    }

    public List<GameObject> SpecialInventorySlotsUI {
        get => specialInventorySlotsUI;
        set => specialInventorySlotsUI = value;
    }

    private void Start() {
        UpdateOrdinarySlots(); UpdateSpecialSlots();
        for (int i = 0; i < ordinaryInventorySlotsUI.Count; i++) {
            ordinaryInventorySlotsUI[i].GetComponent<HandleSlotSelection>().SaveHandler(this, i, false);
        }
        for (int i = 0; i < specialInventorySlotsUI.Count; i++) {
            specialInventorySlotsUI[i].GetComponent<HandleSlotSelection>().SaveHandler(this, i, true);
        }

        CurrentSelectedSlot = ordinaryInventorySlotsUI[0];
        GenerateTestItem();
    }

    public void AddNewItemToInventory(InventoryItem item) {
        if (item.IsSpecialItem) { AddNewSpecialItemToInventory(item); }
        else { AddNewOrdinaryItemToInventory(item); }
    }

    public void AddNewOrdinaryItemToInventory(InventoryItem item) {
        if(inventorySlotsSO.AddOrdinaryItemToInventory(item)) { UpdateOrdinarySlots(); }
    }

    public void AddNewSpecialItemToInventory(InventoryItem item) {
        if(inventorySlotsSO.AddSpecialItemToInventory(item)) { UpdateSpecialSlots(); }
    }
    
    
    public void RemoveItemFromInventory(InventoryItem item, int index) {
        if (item.IsSpecialItem) { RemoveSpecialItemFromInventory(item, index); }
        else { RemoveOrdinaryItemFromInventory(item, index); }
    }
    
    public void RemoveOrdinaryItemFromInventory(InventoryItem item, int index) {
        
    }

    public void RemoveSpecialItemFromInventory(InventoryItem item, int index) {
        
    }

    private void UpdateOrdinarySlots() {
        for (int i = 0; i < inventorySlotsSO.OrdinaryItemsInInventory.Count; i++) {
            ordinaryInventorySlotsUI[i].GetComponent<HandleItemInSlot>()
                .FillSlotWithItem(inventorySlotsSO.OrdinaryItemsInInventory[i]);
        }
    }
    
    private void UpdateSpecialSlots() {
        for (int i = 0; i < inventorySlotsSO.SpecialItemsInInventory.Count; i++) {
            specialInventorySlotsUI[i].GetComponent<HandleItemInSlot>()
                .FillSlotWithItem(inventorySlotsSO.SpecialItemsInInventory[i]);
        }
    }
    
    public void ShowDescriptionSelectedObject(int index, bool isSpecial) {
        InventoryItem itemInSlotSelected = isSpecial
            ? inventorySlotsSO.GetInfoSpecialItemInInventory(index)
            : inventorySlotsSO.GetInfoOrdinaryItemInInventory(index);

        DescriptionText.GetComponent<TextMeshProUGUI>().text = itemInSlotSelected.Description;
        EnergyText.GetComponent<TextMeshProUGUI>().text =
            itemInSlotSelected.EnergyGeneratedOnBurn == 0.0f
                ? " "
                : itemInSlotSelected.EnergyGeneratedOnBurn.ToString();
    }

    public void UnShowDescriptionSelectedObject() {
        DescriptionText.GetComponent<TextMeshProUGUI>().text = " ";
        EnergyText.GetComponent<TextMeshProUGUI>().text = " ";
    }

    public void SetCurrentSelectedSlotByIndex(int index, bool isSlotSpecial) {
        CurrentSelectedSlot.GetComponent<HandleSlotSelection>().ExitHover();
        CurrentSelectedSlot = isSlotSpecial ? specialInventorySlotsUI[index] : ordinaryInventorySlotsUI[index];
    }


    [SerializeField] private Sprite itemSprite;
    private void GenerateTestItem() { AddNewItemToInventory(new InventoryItem("id0", itemSprite, 10,
        "Test Item", 10.0f, false)); }
    
}
