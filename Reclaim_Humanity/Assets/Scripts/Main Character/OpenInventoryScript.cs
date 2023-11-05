using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;

    public void OpenInventory(InputAction.CallbackContext context) {
        if (context.performed) { PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(context.control.name); }
    }
}
