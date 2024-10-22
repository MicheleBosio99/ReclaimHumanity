using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This MonoBehaviour handles the slots inside the inventory.
// FillSlotsWithItem is only called by script HandleItemsInventory whenever the inventory is updated;
public class HandleItemInSlotInv : MonoBehaviour {

    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI quantity;

    [SerializeField] private bool isSlotSpecial;

    public InventoryItem ItemInSlot { get; set; }

    public void FillSlotWithItem(InventoryItem item) {
        ItemInSlot = item;
        itemImage.sprite = item.ItemSprite;
        var color = itemImage.color; color.a = 1.0f; itemImage.color = color;
        if(!isSlotSpecial) { quantity.text = item.ItemQuantity.ToString(); }
    }

    public void UpdateQuantity(int newQuantity) { if(!isSlotSpecial) { quantity.text = newQuantity.ToString(); } }

    public void EmptySlot() {
        ItemInSlot = null;
        itemImage.sprite = null;
        var color = itemImage.color; color.a = 0.0f; itemImage.color = color;
        if(!isSlotSpecial) { quantity.text = " "; }
    }
}