using UnityEngine;


public class SpecialItemBehaviour : MonoBehaviour {
    
    [SerializeField] private int itemQuantity;
    [SerializeField] private ItemsSO itemSO;
    [SerializeField] private AudioClip SpecialItemSound;
    
    private HandleItemsInInventoryInv inventory;
    private GameObject unlockedUI;
    private InventoryItemsSO inventoryItemsSO;

    public ItemsSO ItemSo {
        get => itemSO;
        set => itemSO = value;
    }

    private GameObject parent;
    private ObjectSpawner parentSpawnerReference;

    public void SetPlayerInventoryInstance(GameObject _playerInventory, InventoryItemsSO _inventoryItemsSo) {
        inventory = _playerInventory.GetComponent<HandleItemsInInventoryInv>();
        inventoryItemsSO = _inventoryItemsSo;
    }

    public void OnMouseDown() { AddToInventory(); }

    public void OnTriggerEnter2D(Collider2D other) { if(other.CompareTag("Player")) { AddToInventory(); } }

    private void AddToInventory() {
        SoundFXManager.instance.PlaySoundFXClip(SpecialItemSound, transform,1f);
        inventory.AddNewItemToInventory(itemSO.ToInventoryItem(itemQuantity));
        if (inventoryItemsSO.SpecialItemsInInventory.Count == 3) { PopupFoundAll3SpecialItems(); }
        Destroy(gameObject);
    }

    public void SetUnlockedUI(GameObject _unlockedUI) { unlockedUI = _unlockedUI; }

    private void PopupFoundAll3SpecialItems() {
        GameManager.hasAll3SpecialItems.hasAll3Items = true;
        unlockedUI.GetComponent<VisualizeUnlocked>().StartShowUnlockedRecipeMessage("You found all 3 special items: go back to see Dr.IDK!");
    }
}