using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

// This MonoBehaviour handles the slots inside the inventory.
// FillSlotsWithItem is only called by script HandleItemsInventory whenever the inventory is updated;
public class HandleItemInSlot : MonoBehaviour {

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI quantity;

    [SerializeField] private bool isSlotSpecial;

    public InventoryItem ItemInSlot { get; set; }

    public void FillSlotWithItem(InventoryItem item) {
        Debug.Log("Registered new item insertion: " + item.ToString() + " iSP? " + isSlotSpecial);
        ItemInSlot = item;
        itemImage.sprite = item.ItemSprite;
        var color = itemImage.color; color.a = 1.0f; itemImage.color = color;
        quantity.text = item.ItemQuantity.ToString();
    }

    public void EmptySlot() {
        ItemInSlot = null;
        itemImage.sprite = null;
        var color = itemImage.color; color.a = 0.0f; itemImage.color = color;
        quantity.text = " ";
    }
}