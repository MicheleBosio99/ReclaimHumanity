using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleItemInSlotGE : MonoBehaviour {
    
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI quantity;

    public InventoryItem ItemInSlot { get; set; }

    public void FillSlotWithItem(InventoryItem item) {
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
