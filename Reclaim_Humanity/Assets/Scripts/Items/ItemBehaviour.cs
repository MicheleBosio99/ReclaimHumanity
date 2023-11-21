using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBehaviour : MonoBehaviour, IPointerClickHandler {
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerInventory;
    private HandleItemsInInventoryInv inventory;
    
    [SerializeField] private ItemsSO itemSO;
    private int quantity;

    public void Start() { inventory = playerInventory.GetComponent<HandleItemsInInventoryInv>(); }

    public void OnPointerClick(PointerEventData eventData) { AddToInventory(); }

    public void AddToInventory() {
        if(Vector3.Distance(player.transform.position, this.transform.position) > 10.0f) { return; }
        
        inventory.AddNewItemToInventory(itemSO.ToInventoryItem(quantity));
        Destroy(this);
    }
}
