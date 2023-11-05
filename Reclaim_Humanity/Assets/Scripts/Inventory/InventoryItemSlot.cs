using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This MonoBehaviour handles the slots inside the inventory.
// FillSlotsWithItem is only called by script HandleItemsInventory whenever the inventory is updated;
public class InventoryItemSlot : MonoBehaviour {

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI quantity;

    private void Start() {
        itemImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    public void FillSlotWithItem(InventoryItem item) {
        itemImage.sprite = item.ItemSprite;
        itemImage.color = Color.white;
        quantity.text = item.ItemQuantity.ToString();
    }
}
