using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class HandleItemsInInventoryGE : MonoBehaviour {
    [SerializeField] private LabEnergySO labEnergySO;
    [SerializeField] private InventoryItemsSO inventorySO;
    private List<InventoryItem> inventorySOItems;

    [SerializeField] private List<GameObject> inventorySlotsUI;
    [SerializeField] private List<GameObject> chosenItemSlotsUI;

    private TextsHandler textsHandler;
    private HandleItemInSlotGE _handleItemInSlotGE;
    private float energyCreated;

    private List<HoldSlotUI> holdingSlotsUIList;
    private int maxLengthHoldingSlotsUIList;

    private void Start() {
        textsHandler = gameObject.GetComponent<TextsHandler>();
        _handleItemInSlotGE = gameObject.GetComponent<HandleItemInSlotGE>();
        
        holdingSlotsUIList = new List<HoldSlotUI>();
        maxLengthHoldingSlotsUIList = chosenItemSlotsUI.Count();
    }

    public void OnEnable() {
        inventorySOItems = inventorySO.OrdinaryItemsInInventory;
        energyCreated = 0.0f;
        SetHandler();
        UpdateSlot();
    }

    private void UpdateSlot() {
        for (var i = 0; i < inventorySO.OrdinaryItemsInInventory.Count(); i++) {
            inventorySlotsUI[i].GetComponent<HandleItemInSlotGE>().FillSlotWithItem(inventorySOItems[i]);
        }
    }

    private void SetHandler() {
        for (var i = 0; i < inventorySlotsUI.Count(); i++) {
            inventorySlotsUI[i].GetComponent<HandleSlotSelectionGE>().SaveHandler(this, i, true);
        }
        for (var i = 0; i < chosenItemSlotsUI.Count; i++) {
            chosenItemSlotsUI[i].GetComponent<HandleSlotSelectionGE>().SaveHandler(this, i, false);
        }
    }
    
    public void SlotGotClicked(int index, bool _isInventory) {
        if (_isInventory) { InventorySlotGotClicked(index); }
        else { ChosenSlotGotClicked(index); }
    }

    private void InventorySlotGotClicked(int index) {
        var done = false;
        if (index >= inventorySO.OrdinaryItemsInInventory.Count()) { return; }

        HoldSlotUI slot = null;
        foreach (var hsUI in holdingSlotsUIList) { if (hsUI.IndexInvSlot == index) { slot = hsUI; } } // search if slot already clicked
            
        // List full and that slot not already clicked
        if (holdingSlotsUIList.Count() == maxLengthHoldingSlotsUIList && slot == null) {
            UpdateDescriptionText("", "");
            UpdateEnergyText();
            return;
        }
            
        // Not full list but slot already clicked
        if (slot != null) { done = slot.Swap1InvTOCho(); }
            
        // Still space in list and slot never clicked or emptied
        else {
            var itemInv = inventorySOItems[index];
            var itemCho = itemInv.CreateCopy(); itemCho.ItemQuantity = 0;
            
            slot = new HoldSlotUI(inventorySlotsUI[index], chosenItemSlotsUI[index],
                itemInv, itemCho, index, holdingSlotsUIList.Count());
            
            holdingSlotsUIList.Add(slot);
            done = slot.Swap1InvTOCho();
        }

        if (!done) return;
        energyCreated += slot.ItemInInventory.EnergyGeneratedOnBurn;
        UpdateEnergyText();
        UpdateDescriptionText(slot.ItemInInventory.Description, slot.ItemInInventory.EnergyGeneratedOnBurn.ToString());
    }

    private void ChosenSlotGotClicked(int index) {
        HoldSlotUI slot = null;
        foreach (var hsUI in holdingSlotsUIList) { if (hsUI.IndexChoSlot == index) { slot = hsUI; } }
        if (slot == null) { UpdateDescriptionText("", ""); return; }
        if (!slot.Swap1ChoTOInv()) { holdingSlotsUIList.Remove(slot); }
        
        energyCreated -= slot.ItemInChosen.EnergyGeneratedOnBurn;
        UpdateEnergyText();
        UpdateDescriptionText(slot.ItemInChosen.Description, slot.ItemInChosen.EnergyGeneratedOnBurn.ToString());
    }

    private void UpdateEnergyText() { textsHandler.UpdateEnergy(energyCreated.ToString()); }

    private void UpdateDescriptionText(string description, string energy) { textsHandler.UpdateDescriptionAndEnergy(description, energy); }

    public void OnGenerateEnergyClick() {
        var toRemove = new List<HoldSlotUI>();
        foreach (var slotTuple in holdingSlotsUIList) {
            slotTuple.ResetChosen();
            toRemove.Add(slotTuple);
        }
        holdingSlotsUIList.RemoveAll(slot => true);
        Assert.AreEqual(0, holdingSlotsUIList.Count());
        
        foreach (var slot in toRemove) { holdingSlotsUIList.Remove(slot); }
        
        labEnergySO.CurrentEnergy += energyCreated;
        textsHandler.UpdateEnergy("");
        energyCreated = 0.0f;
        
        inventorySOItems.RemoveAll(slot => slot.ItemQuantity == 0);
        
    }

}


public class HoldSlotUI {

    private GameObject inventorySlotUI;
    private GameObject chosenSlotUI;

    private InventoryItem itemInInventory;
    private InventoryItem itemInChosen;

    private HandleItemInSlotGE inventoryItemHandler;
    private HandleItemInSlotGE chosenItemHandler;

    public int IndexInvSlot { get; }

    public int IndexChoSlot { get; }

    public InventoryItem ItemInInventory => itemInInventory;

    public InventoryItem ItemInChosen => itemInChosen;

    public HoldSlotUI(GameObject inventorySlotUI, GameObject chosenSlotUI, InventoryItem itemInInventory,
        InventoryItem itemInChosen, int indexInvSlot, int indexChoSlot) {
        this.inventorySlotUI = inventorySlotUI;
        this.chosenSlotUI = chosenSlotUI;
        
        inventoryItemHandler = this.inventorySlotUI.GetComponent<HandleItemInSlotGE>();
        chosenItemHandler = this.chosenSlotUI.GetComponent<HandleItemInSlotGE>();
        
        this.itemInInventory = itemInInventory;
        this.itemInChosen = itemInChosen;

        IndexInvSlot = indexInvSlot;
        IndexChoSlot = indexChoSlot;
    }

    public bool Swap1InvTOCho() {
        // if (itemInInventory.ItemQuantity == 0) { return false; }
        //
        // itemInInventory.ItemQuantity --;
        // itemInChosen.ItemQuantity ++;
        //
        // chosenItemHandler.FillSlotWithItem(itemInChosen);
        // inventoryItemHandler.FillSlotWithItem(itemInInventory);
        //
        // return true;
        
        if (itemInInventory.ItemQuantity == 0) { inventoryItemHandler.EmptySlot(); return false; }
        
        itemInInventory.ItemQuantity --;
        itemInChosen.ItemQuantity ++;
        
        chosenItemHandler.FillSlotWithItem(itemInChosen);
        inventoryItemHandler.FillSlotWithItem(itemInInventory);
        return true;
    }
    
    public bool Swap1ChoTOInv() {
        itemInInventory.ItemQuantity ++;
        itemInChosen.ItemQuantity --;
        
        inventoryItemHandler.FillSlotWithItem(itemInInventory);
        
        if (itemInChosen.ItemQuantity == 0) { chosenItemHandler.EmptySlot(); return false; }
        chosenItemHandler.FillSlotWithItem(itemInChosen); return true;
    }

    public void ResetChosen() { chosenItemHandler.EmptySlot(); }

    public override string ToString() {
        return $"invSlotUI: {inventorySlotUI}, choSlotUI: {chosenSlotUI}, itemInInv: {itemInInventory}, " +
               $"itemInCho: {itemInChosen}, indexInvSlot: {IndexInvSlot}, indexChoSlot: {IndexChoSlot}";
    }
}