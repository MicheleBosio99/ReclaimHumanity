using UnityEngine;

public class ItemBehaviour : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private HandleItemsInInventoryInv inventory;
    [SerializeField] private int itemQuantity;
    
    [SerializeField] private ItemsSO itemSO;
    
    private GameObject parent;
    private ObjectSpawner parentSpawnerReference;

    public void SetPlayerInventoryInstance(GameObject _playerInventory) {
        inventory = _playerInventory.GetComponent<HandleItemsInInventoryInv>();
    }

    public void OnMouseDown() { if(Vector3.Distance(player.transform.position, this.transform.position) < 10.0f) {  AddToInventory(); } }

    public void OnTriggerEnter2D(Collider2D other) { if(other.CompareTag("Player")) { AddToInventory(); } }

    public void AddToInventory() {
        inventory.AddNewItemToInventory(itemSO.ToInventoryItem(itemQuantity));
        parentSpawnerReference.ItemGotPickedUp(gameObject);
        Destroy(gameObject);
    }

    public void SetParentObjectReference(GameObject _parent) {
        parent = _parent;
        parentSpawnerReference = parent.GetComponent<ObjectSpawner>();
    }
}
