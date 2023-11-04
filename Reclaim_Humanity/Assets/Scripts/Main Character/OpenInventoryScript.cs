using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject InventoryMenu;

    private void Start() { InventoryMenu.SetActive(false); }

    public void OpenInventory(InputAction.CallbackContext context) {
        if (!InventoryMenu.activeSelf) { InventoryMenu.SetActive(true); }
        else { InventoryMenu.SetActive(false); }
    }
}
