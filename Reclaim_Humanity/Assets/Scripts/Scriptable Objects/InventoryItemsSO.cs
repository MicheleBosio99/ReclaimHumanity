using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySlotsSO", menuName = "ScriptableObjects/InventorySlotsSO")]
public class InventoryItemsSO : ScriptableObject {

    private List<InventoryItem> ordinaryItemsInInventory = new List<InventoryItem>(); // Should be loaded from saves
    private List<InventoryItem> specialItemsInInventory = new List<InventoryItem>(); // Should be loaded from saves

    public List<InventoryItem> OrdinaryItemsInInventory { get => ordinaryItemsInInventory; }
    public List<InventoryItem> SpecialItemsInInventory { get => specialItemsInInventory; }
    public int MaxOrdinarySlots { get; } = 15;
    public int MaxSpecialSlots { get; } = 3;


    // Called by some script on player that will detect collisions with items and pick them up;
    public bool AddOrdinaryItemToInventory(InventoryItem item) {
        if (ordinaryItemsInInventory.Count >= MaxOrdinarySlots) return false;
        
        var notNewItem = SearchOrdinaryItemByID(item.ItemID);
        if (notNewItem.ItemQuantity > 0) { notNewItem.ItemQuantity += item.ItemQuantity; }
        else { ordinaryItemsInInventory.Add(item); }
        return true;
    }
    
    public bool AddSpecialItemToInventory(InventoryItem item) {
        if (specialItemsInInventory.Count >= MaxSpecialSlots) return false;
        specialItemsInInventory.Add(item); return true;
        // Probably needs changes, not now
    }

    public InventoryItem GetInfoOrdinaryItemInInventory(int index) {
        return index >= ordinaryItemsInInventory.Count ? InventoryItem.GetEmptyItem() : ordinaryItemsInInventory[index];
    }

    public InventoryItem GetInfoSpecialItemInInventory(int index) {
        return index >= specialItemsInInventory.Count ? InventoryItem.GetEmptyItem() : specialItemsInInventory[index];
    }

    public InventoryItem SearchOrdinaryItemByID(string itemID) {
        foreach (var item in ordinaryItemsInInventory) { if (item.ItemID == itemID) { return item; } }
        return InventoryItem.GetEmptyItem();
    }
    
    public InventoryItem SearchSpecialItemByID(string itemID) {
        foreach (var item in specialItemsInInventory) { if (item.ItemID == itemID) { return item; } }
        // var itemRet = InventoryItem.GetEmptyItem(); itemRet.IsSpecialItem = true; return itemRet;
        return null;
    }

    public void ChangeQuantityOrdinaryItem(InventoryItem item, int quantity) {
        if (item.ItemQuantity < quantity) { throw new NotEnoughItemsInInventoryException(); }
        if (item.ItemQuantity == quantity) { ordinaryItemsInInventory.Remove(item); }
        else { item.ItemQuantity -= quantity; }
    }

    public void Delete0QuantityElement() { ordinaryItemsInInventory.RemoveAll(item => item.ItemQuantity == 0); }
}

public class NotEnoughItemsInInventoryException : Exception {
    public NotEnoughItemsInInventoryException() { }
    public NotEnoughItemsInInventoryException(string message) : base(message) { }
    public NotEnoughItemsInInventoryException(string message, Exception inner) : base(message, inner) { }
}

public class InventoryItem {

    private string itemID;
    private Sprite itemSprite;
    private int itemQuantity;
    private string description;
    private float energyGeneratedOnBurn;
    private bool isSpecialItem;

    public InventoryItem(string itemID, Sprite itemSprite, int itemQuantity,
            string description, float energyGeneratedOnBurn, bool isSpecialItem) {
        this.itemID = itemID;
        this.itemSprite = itemSprite;
        this.itemQuantity = itemQuantity;
        this.description = description;
        this.energyGeneratedOnBurn = energyGeneratedOnBurn;
        this.isSpecialItem = isSpecialItem;
    }

    public InventoryItem CreateCopy() {
        return new InventoryItem(itemID, ItemSprite, ItemQuantity, description,
            energyGeneratedOnBurn, isSpecialItem);
    }

    public static InventoryItem GetEmptyItem() {
        return new InventoryItem("", null, 0, "", 0.0f, false);
    }

    public string ItemID {
        get => itemID;
        set => itemID = value;
    }

    public Sprite ItemSprite {
        get => itemSprite;
        set => itemSprite = value;
    }

    public int ItemQuantity {
        get => itemQuantity;
        set => itemQuantity = value;
    }

    public string Description {
        get => description;
        set => description = value;
    }

    public float EnergyGeneratedOnBurn {
        get => energyGeneratedOnBurn;
        set => energyGeneratedOnBurn = value;
    }

    public bool IsSpecialItem {
        get => isSpecialItem;
        set => isSpecialItem = value;
    }

    public override string ToString() {
        return $"ID: {ItemID}, Quantity: {itemQuantity}, Description: {Description}, Energy: {EnergyGeneratedOnBurn}, Special: {IsSpecialItem}";
    }
}