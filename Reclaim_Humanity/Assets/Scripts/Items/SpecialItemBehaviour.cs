using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecialItemBehaviour : MonoBehaviour {
    
    private HandleItemsInInventoryInv inventory;
    [SerializeField] private int itemQuantity;
    
    [SerializeField] private ItemsSO itemSO;

    public ItemsSO ItemSo {
        get => itemSO;
        set => itemSO = value;
    }

    private GameObject parent;
    private ObjectSpawner parentSpawnerReference;

    public void SetPlayerInventoryInstance(GameObject _playerInventory) { inventory = _playerInventory.GetComponent<HandleItemsInInventoryInv>(); }

    public void OnMouseDown() { AddToInventory(); }

    public void OnTriggerEnter2D(Collider2D other) { if(other.CompareTag("Player")) { AddToInventory(); } }

    public void AddToInventory() {
        inventory.AddNewItemToInventory(itemSO.ToInventoryItem(itemQuantity));
        Destroy(gameObject);
    }
}