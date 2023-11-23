using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerInventory;
    private HandleItemsInInventoryInv inventory;
    [SerializeField] private int itemQuantity;
    
    [SerializeField] private ItemsSO itemSO;

    public void Start() { inventory = playerInventory.GetComponent<HandleItemsInInventoryInv>(); }

    public void OnMouseDown() { if(Vector3.Distance(player.transform.position, this.transform.position) < 10.0f) {  AddToInventory(); } }

    public void OnTriggerEnter2D(Collider2D other) { if(other.CompareTag("Player")) { AddToInventory(); } }

    public void AddToInventory() {
        inventory.AddNewItemToInventory(itemSO.ToInventoryItem(itemQuantity));
        Destroy(gameObject);
    }
}
