using System;
using System.Collections;
using UnityEngine;

public class ChangeMenuShowed : MonoBehaviour {
    
    // private void Start() { DontDestroyOnLoad(gameObject); }

    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject StatisticsPanel;
    [SerializeField] private GameObject MapPanel;

    [SerializeField] private GameObject OpenPlayerMenuButton;

    [SerializeField] private GameObject BackgroundGeneralPanel;
    
    [SerializeField] private GameObject player;

    private GameObject currentEnabledGameObj;

    public GameObject CurrentEnabledGameObj => currentEnabledGameObj;

    private void Awake() {
        InventoryPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        StatisticsPanel.SetActive(false);
        MapPanel.SetActive(false);
        
        BackgroundGeneralPanel.SetActive(false);
    }

    public void GetKeyPressed(string keyPressed) {
        // Stop time for menu open
        Time.timeScale = 0.0f;
        
        OpenPlayerMenuButton.SetActive(false);
        BackgroundGeneralPanel.SetActive(true);
        
        // TODO CHECK BUTTONS ON GAMEPAD NAMES!!!
        switch (keyPressed) {
            case "i" or "button north":
                if (currentEnabledGameObj == InventoryPanel) { OnCloseButtonClick(); }
                else { OnInventoryButtonClick(); }
                break;
            case "escape" or "start":
                if (currentEnabledGameObj == OptionsPanel) { OnCloseButtonClick(); }
                else { OnOptionsButtonClick(); }
                break;
            case "k" or "button west":
                if (currentEnabledGameObj == StatisticsPanel) { OnCloseButtonClick(); }
                else { OnStatisticsButtonClick(); }
                break;
            case "m" or "button east":
                if (currentEnabledGameObj == MapPanel) { OnCloseButtonClick(); }
                else { OnMapButtonClick(); }
                break;
        }
    }
    
    public void OnCloseButtonClick() {
        // Restart timer
        Time.timeScale = 1.0f;
        
        currentEnabledGameObj.SetActive(false);
        currentEnabledGameObj = null;
        OpenPlayerMenuButton.SetActive(true);
        BackgroundGeneralPanel.SetActive(false);
        
        
        player.GetComponent<OpenInventoryScript>().ClosedInventory();
    }
    
    private void ChangeMenuScreen(GameObject objToEnable) {
        if(currentEnabledGameObj != null) { currentEnabledGameObj.SetActive(false); }
        currentEnabledGameObj = objToEnable;
        objToEnable.SetActive(true);
    }
    
    public void OnInventoryButtonClick() { ChangeMenuScreen(InventoryPanel); }
    
    public void OnOptionsButtonClick() { ChangeMenuScreen(OptionsPanel); }
    
    public void OnStatisticsButtonClick() { ChangeMenuScreen(StatisticsPanel); }
    
    public void OnMapButtonClick() { ChangeMenuScreen(MapPanel); }
    
    public void OnOpenMenuClick() { GetKeyPressed("escape"); }
}
