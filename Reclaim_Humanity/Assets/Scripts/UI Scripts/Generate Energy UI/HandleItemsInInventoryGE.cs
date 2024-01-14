using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class HandleItemsInInventoryGE : MonoBehaviour {
    [SerializeField] private GameObject labEnergySOSetter;
    [SerializeField] private InventoryItemsSO inventorySO;
    private List<InventoryItem> inventorySOItems;

    [SerializeField] private List<GameObject> inventorySlotsUI;
    [SerializeField] private List<GameObject> chosenItemSlotsUI;
    
    [SerializeField] private GameObject PowerUpsManager;
    [SerializeField] private GameObject GenerateCanvasManager;
    [SerializeField] private GameObject unlockUI;
    
    [SerializeField] private GameObject batteryEnergy;
    private ShowEnergyLab batteryEnergyText;

    private TextsHandler textsHandler;
    private HandleItemInSlotGE _handleItemInSlotGE;
    private LabEnergySOSetter energySoSetter;
    private float energyCreated;

    private List<HoldSlotUI> holdingSlotsUIList;
    private int maxLengthHoldingSlotsUIList;
    
    private PowerUpsManager powerUpsManager;
    
    private bool isFinalPotionIn = false;

    private void Start() {
        textsHandler = gameObject.GetComponent<TextsHandler>();
        _handleItemInSlotGE = gameObject.GetComponent<HandleItemInSlotGE>();
        batteryEnergyText = batteryEnergy.GetComponent<ShowEnergyLab>();
        energySoSetter = labEnergySOSetter.GetComponent<LabEnergySOSetter>();
        // powerUpsManager = new PowerUpsManager(energySoSetter.GetCurrentEnergy());
        
        holdingSlotsUIList = new List<HoldSlotUI>();
        maxLengthHoldingSlotsUIList = chosenItemSlotsUI.Count();
    }

    public void OnEnable() {
        if (inventorySO.SpecialItemsInInventory.Any((item) => item.ItemID == "S_FinalPotion")) { isFinalPotionIn = true; }

        inventorySOItems = inventorySO.OrdinaryItemsInInventory;
        energyCreated = 0.0f;
        SetHandler();
        UpdateSlot();
    }

    public void OnDisable() { if (holdingSlotsUIList != null) { foreach (var slot in holdingSlotsUIList) { slot.ResetSlot(); } } }

    private void UpdateSlot() {
        var i = 0;
        for (i = 0; i < inventorySO.OrdinaryItemsInInventory.Count(); i++) {
            inventorySlotsUI[i].GetComponent<HandleItemInSlotGE>().FillSlotWithItem(inventorySOItems[i]);
        }
        for (var j = i; j < inventorySO.MaxOrdinarySlots; j++) {
            inventorySlotsUI[i].GetComponent<HandleItemInSlotGE>().EmptySlot();
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
    
    public void SlotGotClicked(int index, bool _isInventory, bool shift) {
        if (_isInventory) { InventorySlotGotClicked(index, shift); }
        else { ChosenSlotGotClicked(index, shift); }
    }

    private void InventorySlotGotClicked(int index, bool shift) {
        Tuple<bool, int> resultTuple;
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
        if (slot != null) { resultTuple = slot.Swap1InvTOCho(shift); }
        
        // Still space in list and slot never clicked or emptied
        else {
            var itemInv = inventorySOItems[index];
            var itemCho = itemInv.CreateCopy(); itemCho.ItemQuantity = 0;
            
            var choIndex = holdingSlotsUIList.Count();
            slot = new HoldSlotUI(inventorySlotsUI[index], chosenItemSlotsUI[choIndex],
                itemInv, itemCho, index, choIndex);
            
            holdingSlotsUIList.Add(slot);
            resultTuple = slot.Swap1InvTOCho(shift);
        }
        
        var done = resultTuple.Item1;

        if (!done) return;
        energyCreated += (slot.ItemInInventory.EnergyGeneratedOnBurn) * resultTuple.Item2;
        UpdateEnergyText();
        UpdateDescriptionText(slot.ItemInInventory.Description, slot.ItemInInventory.EnergyGeneratedOnBurn.ToString());
    }

    private void ChosenSlotGotClicked(int index, bool shift) {
        HoldSlotUI slot = null;
        foreach (var hsUI in holdingSlotsUIList) { if (hsUI.IndexChoSlot == index) { slot = hsUI; } }
        if (slot == null) { UpdateDescriptionText("", ""); return; }
        
        var resultTuple = slot.Swap1ChoTOInv(shift);
        if (!resultTuple.Item1) { holdingSlotsUIList.Remove(slot); }
        
        energyCreated -= (slot.ItemInChosen.EnergyGeneratedOnBurn) * resultTuple.Item2;
        UpdateEnergyText();
        UpdateDescriptionText(slot.ItemInChosen.Description, slot.ItemInChosen.EnergyGeneratedOnBurn.ToString());
    }

    private void UpdateEnergyText() { textsHandler.UpdateEnergy(energyCreated.ToString()); }

    private void UpdateDescriptionText(string description, string energy) { textsHandler.UpdateDescriptionAndEnergy(description, energy); }

    public void OnGenerateEnergyClick() {
        foreach (var slotTuple in holdingSlotsUIList) { slotTuple.EmptyChosenSlot(); }
        holdingSlotsUIList.RemoveAll(slot => true);
        
        energySoSetter.UpdateEnergySO(energyCreated);
        GameManager.energyInLab = energySoSetter.GetCurrentEnergy();
        batteryEnergyText.UpdateEnergy(energySoSetter.GetCurrentEnergy().ToString());
        
        PowerUpsManager.GetComponent<PowerUpsManager>().UpdatePowerUps();

        textsHandler.UpdateEnergy("");
        energyCreated = 0.0f;
        
        inventorySOItems.RemoveAll(slot => slot.ItemQuantity == 0);
        
        GenerateCanvasManager.GetComponent<GenerateEnergyCanvasHandler>().OnCloseButtonClick();
        
        if (isFinalPotionIn && inventorySO.SpecialItemsInInventory.All(item => item.ItemID != "S_FinalPotion")) {
            FinalPotionHasBeenBurned();
        }
    }

    private void FinalPotionHasBeenBurned() {
        unlockUI.GetComponent<VisualizeUnlocked>().StartShowUnlockedPowerUpMessage("You ************. You burned the Final Potion. How are you gonna finish the game now??");
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

    public Tuple<bool, int> Swap1InvTOCho(bool shift) {
        // return Swap1InvToChoBody();
        if (!shift) { return new Tuple<bool, int>(Swap1InvToChoBody(), 1); }
        
        var times = itemInInventory.ItemQuantity;
        for (var i = 0; i < times; i++) { var res = Swap1InvToChoBody(); }
        
        return new Tuple<bool, int>(true, times);
    }

    private bool Swap1InvToChoBody() {
        switch (itemInInventory.ItemQuantity) {
            case 0: return false;
        
            case 1:
                itemInInventory.ItemQuantity --;
                itemInChosen.ItemQuantity ++;
        
                inventoryItemHandler.EmptySlot();
                chosenItemHandler.FillSlotWithItem(itemInChosen);
                return true;
        
            default:
                itemInInventory.ItemQuantity --;
                itemInChosen.ItemQuantity ++;
    
                chosenItemHandler.FillSlotWithItem(itemInChosen);
                inventoryItemHandler.FillSlotWithItem(itemInInventory);
                return true;
        }
    }

    public Tuple<bool, int> Swap1ChoTOInv(bool shift) {
        return new Tuple<bool, int>(Swap1ChoTOInvBody(), 1);
        
        // if (!shift) { return new Tuple<bool, int>(Swap1ChoTOInvBody(), 1); }
        //
        // var times = itemInInventory.ItemQuantity;
        // for (var i = 0; i < times; i++) { var res = Swap1ChoTOInvBody(); }
        //
        // return new Tuple<bool, int>(true, times);
    }

    private bool Swap1ChoTOInvBody() {
        itemInInventory.ItemQuantity ++;
        itemInChosen.ItemQuantity --;
        
        inventoryItemHandler.FillSlotWithItem(itemInInventory);
        
        if (itemInChosen.ItemQuantity == 0) { chosenItemHandler.EmptySlot(); return false; }
        chosenItemHandler.FillSlotWithItem(itemInChosen); return true;
    }

    public void EmptyChosenSlot() { chosenItemHandler.EmptySlot(); }

    public void ResetSlot() {
        chosenItemHandler.EmptySlot();
        inventoryItemHandler.EmptySlot();
        
        itemInInventory.ItemQuantity += itemInChosen.ItemQuantity;
        itemInChosen.ItemQuantity = 0;
    }

    public override string ToString() {
        return $"invSlotUI: {inventorySlotUI}, choSlotUI: {chosenSlotUI}, itemInInv: {itemInInventory}, " +
               $"itemInCho: {itemInChosen}, indexInvSlot: {IndexInvSlot}, indexChoSlot: {IndexChoSlot}";
    }
}