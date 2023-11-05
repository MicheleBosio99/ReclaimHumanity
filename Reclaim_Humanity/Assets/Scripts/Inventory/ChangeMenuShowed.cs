using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuShowed : MonoBehaviour {

    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject WeaponsPanel;
    [SerializeField] private GameObject CompanionsPanel;
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;

    private GameObject currentEnabledGameObj;

    private void Start() { playerMovement = player.GetComponent<PlayerMovement>(); }

    private void OnEnable() {
        InventoryPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        WeaponsPanel.SetActive(false);
        CompanionsPanel.SetActive(false);
        
        currentEnabledGameObj = InventoryPanel;

        playerMovement.CurrentSpeed = 0.0f;
    }

    private void OnDisable() { playerMovement.CurrentSpeed = playerMovement.NormalSpeed; }

    private void Update() {
        //if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.F)) { OnCloseButtonClick(); }
    }
    
    private void ChangeMenuScreen(GameObject objToEnable) {
        currentEnabledGameObj.SetActive(false);
        currentEnabledGameObj = objToEnable;
        objToEnable.SetActive(true);
    }

    public void OnCloseButtonClick() { gameObject.SetActive(false); }
    
    public void OnInventoryButtonClick() { ChangeMenuScreen(InventoryPanel); }
    
    public void OnOptionsButtonClick() { ChangeMenuScreen(OptionsPanel); }
    
    public void OnWeaponsButtonClick() { ChangeMenuScreen(WeaponsPanel); }
    
    public void OnCompanionsButtonClick() { ChangeMenuScreen(CompanionsPanel); }
}
