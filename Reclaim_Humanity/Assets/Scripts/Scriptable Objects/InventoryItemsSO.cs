using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySlotsSO", menuName = "ScriptableObjects/InventorySlotsSO")]
public class InventoryItemsSO : ScriptableObject {

    private List<InventoryItem> ordinaryItemsInInventory = new List<InventoryItem>(); // Should be loaded from saves
    private List<InventoryItem> specialItemsInInventory = new List<InventoryItem>(); // Should be loaded from saves

    private int maxOrdinarySlots = 15; // Should be loaded from .json properties
    private int maxSpecialSlots = 3; // Should be loaded from .json properties

    public List<InventoryItem> OrdinaryItemsInInventory { get => ordinaryItemsInInventory; }
    public List<InventoryItem> SpecialItemsInInventory { get => specialItemsInInventory; }
    public int MaxOrdinarySlots { get => maxOrdinarySlots; }
    public int MaxSpecialSlots { get => maxSpecialSlots; }
    

    // Called by some script on player that will detect collisions with items and pick them up;
    public bool AddOrdinaryItemToInventory(InventoryItem item) {
        if (ordinaryItemsInInventory.Count >= maxOrdinarySlots) return false;
        ordinaryItemsInInventory.Add(item); return true;
    }
    
    public bool AddSpecialItemToInventory(InventoryItem item) {
        if (specialItemsInInventory.Count >= maxSpecialSlots) return false;
        specialItemsInInventory.Add(item); return true;
    }
}

public class InventoryItem {

    private string itemID;
    private Sprite itemSprite;
    private int itemQuantity;

    public InventoryItem(string itemID, Sprite itemSprite, int itemQuantity) {
        this.itemID = itemID;
        this.itemSprite = itemSprite;
        this.itemQuantity = itemQuantity;
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
}