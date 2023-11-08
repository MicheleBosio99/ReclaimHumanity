using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;

    public void OpenInventory(InputAction.CallbackContext context) {
        PlayerMenu.SetActive(true);
        if (context.performed) { PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(context.control.name); }
    }

    public GameObject CurrentlyToOpenUI { get; set; }
    private bool isActive = false;

    public void OpenCloseUI(InputAction.CallbackContext context) {
        if (CurrentlyToOpenUI == null) { return; }
        if (context.performed) {
            if (isActive) { CurrentlyToOpenUI.SetActive(false); isActive = false; }
            else { CurrentlyToOpenUI.SetActive(true); isActive = true; }
        }
    }
}
