using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class HandleItemsInInventoryInv : MonoBehaviour {
    
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
        for (int i = 0; i < ordinaryInventorySlotsUI.Count; i++) {
            ordinaryInventorySlotsUI[i].GetComponent<HandleSlotSelectionInv>().SaveHandler(this, i, false);
        }
        for (int i = 0; i < specialInventorySlotsUI.Count; i++) {
            specialInventorySlotsUI[i].GetComponent<HandleSlotSelectionInv>().SaveHandler(this, i, true);
        }

        CurrentSelectedSlot = ordinaryInventorySlotsUI[0];
        GenerateTestItem();
    }

    private void OnEnable() { UpdateOrdinarySlots(); UpdateSpecialSlots(); }

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
        var i = 0;
        for (i = 0; i < inventorySlotsSO.OrdinaryItemsInInventory.Count; i ++) {
            ordinaryInventorySlotsUI[i].GetComponent<HandleItemInSlotInv>()
                .FillSlotWithItem(inventorySlotsSO.OrdinaryItemsInInventory[i]);
        }
        for (var j = i; j < inventorySlotsSO.MaxOrdinarySlots; j ++) {
            ordinaryInventorySlotsUI[j].GetComponent<HandleItemInSlotInv>().EmptySlot();
        }
    }
    
    private void UpdateSpecialSlots() {
        var i = 0;
        for (i = 0; i < inventorySlotsSO.SpecialItemsInInventory.Count; i ++) {
            specialInventorySlotsUI[i].GetComponent<HandleItemInSlotInv>()
                .FillSlotWithItem(inventorySlotsSO.SpecialItemsInInventory[i]);
        }
        for (var j = i; j < inventorySlotsSO.MaxSpecialSlots; j ++) {
            specialInventorySlotsUI[j].GetComponent<HandleItemInSlotInv>().EmptySlot();
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
        CurrentSelectedSlot.GetComponent<HandleSlotSelectionInv>().ExitHover();
        CurrentSelectedSlot = isSlotSpecial ? specialInventorySlotsUI[index] : ordinaryInventorySlotsUI[index];
    }


    [SerializeField] private Sprite itemSprite1;
    [SerializeField] private Sprite itemSprite2;
    private void GenerateTestItem() {
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest1").ToInventoryItem(9));
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest2").ToInventoryItem(6));
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest3").ToInventoryItem(4));
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest4").ToInventoryItem(7));
        
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest5").ToInventoryItem(12));
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest6").ToInventoryItem(3));
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest7").ToInventoryItem(5));
        AddNewItemToInventory(Resources.Load<ItemsSO>($"Items/itemTest8").ToInventoryItem(10));
    }
    
}