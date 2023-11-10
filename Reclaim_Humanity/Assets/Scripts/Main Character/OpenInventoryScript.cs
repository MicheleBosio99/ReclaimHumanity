using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;
    
    private bool inventoryIsOpen = false;
    
    public void OpenInventory(InputAction.CallbackContext context) {
        PlayerMenu.SetActive(true);
        if (context.performed && !isActive) {
            inventoryIsOpen = true;
            PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(context.control.name);
        }
    }

    public void ClosedInventory() { inventoryIsOpen = false; }

    public GameObject CurrentlyToOpenUI { get; set; }
    private bool isActive = false;

    public void OpenCloseUI(InputAction.CallbackContext context) { if(!inventoryIsOpen) { OpenCloseUIFunc(context.performed); } }

    public void OpenCloseUIFunc(bool performed) {
        if (CurrentlyToOpenUI == null || !performed) { return; }
        if (isActive) { CurrentlyToOpenUI.SetActive(false); isActive = false; }
        else { CurrentlyToOpenUI.SetActive(true); isActive = true; }
    }
}
