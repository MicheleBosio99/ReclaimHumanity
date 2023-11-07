using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;

    public void OpenInventory(InputAction.CallbackContext context) {
        PlayerMenu.SetActive(true);
        if (context.performed) { PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(context.control.name); }
    }

    public GameObject CurrentlyOpenUI { get; set; }
}
