using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;
    [SerializeField] private GameObject OptionsOverlay;
    [SerializeField] private AudioClip PlayerMenuSound;
    
    private PlayerMovement playerMov;
    public bool isActive;
    private bool inventoryIsOpen;
    private bool finished;
    
    public bool Finished { set => finished = value; }

    private void Awake() {
        playerMov = gameObject.GetComponent<PlayerMovement>();
        finished = true;
        isActive = false;
        inventoryIsOpen = false;
    }
    
    private void OnEnable() { finished = true; }

    public void OpenInventory(InputAction.CallbackContext context) {
        if (!context.performed || isActive) return;
        OpenInventoryBody(context.control.name);
    }

    public void OpenInventoryBody(string keyPressed) {
        PlayerMenu.SetActive(true);
        inventoryIsOpen = true;
        PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(keyPressed);
    }

    public void ClosedInventory() { inventoryIsOpen = false; isActive = false; }

    public GameObject CurrentlyToOpenUI { get; set; }

    public void OpenCloseUI(InputAction.CallbackContext context) { if(!inventoryIsOpen) { OpenCloseUIFunc(context.performed); } }

    public void OpenCloseUIFunc(bool performed) {
        if (CurrentlyToOpenUI == null || !performed || !finished) { return; }
        if (isActive) { CurrentlyToOpenUI.SetActive(false); isActive = false; BlockPlayer(false); }
        else { CurrentlyToOpenUI.SetActive(true); isActive = true; BlockPlayer(true); }
        
        // Play Open-close sound
        SoundFXManager.instance.PlaySoundFXClip(PlayerMenuSound, transform,1f);
    }

    public void CloseTeleportUI() {
        isActive = false;
        finished = true;
        BlockPlayer(false);
    }

    public void BlockPlayer(bool blockPlayer) {
        playerMov.enabled = !blockPlayer;
        OptionsOverlay.SetActive(!blockPlayer);
    }
    
    // private float lastLogTime = 0.0f;
    //
    // private void Update() {
    //     
    //     if (!(Time.time - lastLogTime >= 2.0f)) return;
    //     
    //     Debug.Log($"inventoryIsOpen: {inventoryIsOpen}");
    //     // Debug.Log($"isActive: {isActive}");
    //     // Debug.Log($"CurrentlyToOpenUI: {CurrentlyToOpenUI}");
    //         
    //     lastLogTime = Time.time;
    // }
}
