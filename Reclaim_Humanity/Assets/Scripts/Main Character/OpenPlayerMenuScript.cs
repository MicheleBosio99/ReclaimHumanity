using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;
    [SerializeField] private GameObject OpenPlayerMenuButton;
    [SerializeField] private AudioClip PlayerMenuSound;
    
    // private bool inventoryIsOpen = false;

    public void OpenInventory(InputAction.CallbackContext context) {
        if (!context.performed || isActive) return;
        
        PlayerMenu.SetActive(true);
        // inventoryIsOpen = true;
        PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(context.control.name);
    }

    public void ClosedInventory() {
        // inventoryIsOpen = false;
    }

    public GameObject CurrentlyToOpenUI { get; set; }
    private bool isActive = false;

    public void OpenCloseUI(InputAction.CallbackContext context) {
        // if(!inventoryIsOpen) { OpenCloseUIFunc(context.performed); } inventoryIsOpen always true cannot understand why;
        OpenCloseUIFunc(context.performed);
    }

    public void OpenCloseUIFunc(bool performed) {
        if (CurrentlyToOpenUI == null || !performed) { return; }
        if (isActive) { CurrentlyToOpenUI.SetActive(false); isActive = false; BlockPlayer(false); }
        else { CurrentlyToOpenUI.SetActive(true); isActive = true; BlockPlayer(true); }
        
        SoundFXManager.instance.PlaySoundFXClip(PlayerMenuSound, transform,1f);
    }

    private void BlockPlayer(bool blockPlayer) {
        var playerMov = gameObject.GetComponent<PlayerMovement>();
        playerMov.CurrentSpeed = blockPlayer ? 0.0f : playerMov.NormalSpeed;
        
        OpenPlayerMenuButton.SetActive(!blockPlayer);
    }
}