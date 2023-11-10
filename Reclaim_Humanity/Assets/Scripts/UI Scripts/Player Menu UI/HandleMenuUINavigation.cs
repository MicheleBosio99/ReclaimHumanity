using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleMenuUI : MonoBehaviour {
    
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject StatisticsPanel;
    [SerializeField] private GameObject MapPanel;

    private HandleNavigationInventoryInv _handlerInventoryInv;
    private HandleNavigationOptions handlerOptions;
    private HandleNavigationStatistics handlerStatistics;
    private HandleNavigationMap handlerMap;
    
    [SerializeField] private GameObject PlayerMenu;
    private ChangeMenuShowed menuShowedHandler;

    private GameObject currentEnabledMenu;

    private void Awake() {
        menuShowedHandler = PlayerMenu.GetComponent<ChangeMenuShowed>();

        _handlerInventoryInv = InventoryPanel.GetComponent<HandleNavigationInventoryInv>();
        handlerOptions = OptionsPanel.GetComponent<HandleNavigationOptions>();
        handlerStatistics = StatisticsPanel.GetComponent<HandleNavigationStatistics>();
        handlerMap = MapPanel.GetComponent<HandleNavigationMap>();
    }


    public void UINavigation(InputAction.CallbackContext context) {
        currentEnabledMenu = menuShowedHandler.CurrentEnabledGameObj;
        if (currentEnabledMenu == null) { return; }
        
        var movement = context.ReadValue<Vector2>();
        if (context.performed) {
            if (currentEnabledMenu == InventoryPanel) { _handlerInventoryInv.GetNavigationInput(movement); }
            else if (currentEnabledMenu == OptionsPanel) { handlerOptions.GetNavigationInput(movement); }
            else if (currentEnabledMenu == StatisticsPanel) { handlerStatistics.GetNavigationInput(movement); }
            else if (currentEnabledMenu == MapPanel) { handlerMap.GetNavigationInput(movement); }
        }
    }
}
