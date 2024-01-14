using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SpecialObjectSpawner : MonoBehaviour {
    
    [SerializeField] private InventoryItemsSO inventorySO;
    [SerializeField] private GameObject specialItem;
    [SerializeField] private GameObject playerInventory;
    [SerializeField] private GameObject UnlockedUI;

    private void Start() {
        if (inventorySO.SearchSpecialItemByID(specialItem.GetComponent<SpecialItemBehaviour>().ItemSo.ItemID) == null) { SpawnSpecialItem(); }
    }

    private void SpawnSpecialItem() {
        var parent = gameObject;
        var newItem = Instantiate(specialItem, parent.transform.position, Quaternion.identity, parent.transform);
        
        var itemBehaviour = newItem.GetComponent<SpecialItemBehaviour>();
        itemBehaviour.SetPlayerInventoryInstance(playerInventory);
        itemBehaviour.SetUnlockedUI(UnlockedUI);
    }

    private void ItemGotPickedUp() {
        
    }
}
