using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventoryScript : MonoBehaviour {
    
    [SerializeField] private GameObject PlayerMenu;
    [SerializeField] private GameObject OpenPlayerMenuButton;
    [SerializeField] private AudioClip PlayerMenuSound;
    
    private bool inventoryIsOpen;
    [SerializeField] private AudioClip PlayerMenuSound;
    

    public void OpenInventory(InputAction.CallbackContext context) {
        if (!context.performed || isActive) return;
        
        PlayerMenu.SetActive(true);
        inventoryIsOpen = true;
        PlayerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed(context.control.name);
    }

    public void ClosedInventory() { inventoryIsOpen = false; }

    public GameObject CurrentlyToOpenUI { get; set; }
    
    public bool isActive;
    private bool finished; public bool Finished { set => finished = value; }

    private void OnEnable() { finished = true; }

    public void OpenCloseUI(InputAction.CallbackContext context) {
        // if(!inventoryIsOpen) { OpenCloseUIFunc(context.performed); } inventoryIsOpen always true cannot understand why;
        OpenCloseUIFunc(context.performed);
        
        //Play open-close sound
        SoundFXManager.instance.PlaySoundFXClip(PlayerMenuSound, transform,1f);
    }

    public void OpenCloseUIFunc(bool performed) {
        if (CurrentlyToOpenUI == null || !performed || !finished) { return; }
        if (isActive) { CurrentlyToOpenUI.SetActive(false); isActive = false; BlockPlayer(false); }
        else { CurrentlyToOpenUI.SetActive(true); isActive = true; BlockPlayer(true); }
        
        SoundFXManager.instance.PlaySoundFXClip(PlayerMenuSound, transform,1f);
    }
    
    private PlayerMovement playerMov;
    private void Awake() { playerMov = gameObject.GetComponent<PlayerMovement>(); }

    public void BlockPlayer(bool blockPlayer) {
        playerMov.enabled = !blockPlayer;
        OpenPlayerMenuButton.SetActive(!blockPlayer);
    }
    
    // private float lastLogTime = 0.0f;
    //
    // private void Update() {
    //     
    //     if (!(Time.time - lastLogTime >= 1.0f)) return;
    //     
    //     Debug.Log($"Finished: {finished}");
    //     Debug.Log($"isActive: {isActive}");
    //     Debug.Log($"CurrentlyToOpenUI: {CurrentlyToOpenUI}");
    //         
    //     lastLogTime = Time.time;
    // }
}
