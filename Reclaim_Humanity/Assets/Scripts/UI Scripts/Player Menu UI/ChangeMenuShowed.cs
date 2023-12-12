using UnityEngine;

public class ChangeMenuShowed : MonoBehaviour {
    
    // private void Start() { DontDestroyOnLoad(gameObject); }

    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject StatisticsPanel;
    [SerializeField] private GameObject MapPanel;
    [SerializeField] private GameObject TutorialPanel;

    [SerializeField] private GameObject LayoutButtons;

    [SerializeField] private GameObject BackgroundGeneralPanel;
    
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip OpenMenuSound;
    [SerializeField] private AudioClip CloseMenuSound;

    private GameObject currentEnabledGameObj;

    public GameObject CurrentEnabledGameObj => currentEnabledGameObj;

    private void Awake() {
        InventoryPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        StatisticsPanel.SetActive(false);
        MapPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        
        BackgroundGeneralPanel.SetActive(false);
    }

    public void GetKeyPressed(string keyPressed) {
        // Stop time for menu open
        Time.timeScale = 0.0f;
        
        LayoutButtons.SetActive(false);
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
            case "t":
                if (currentEnabledGameObj == TutorialPanel) { OnCloseButtonClick(); }
                else { OnTutorialButtonClick(); }
                break;
            case "close":
                OnCloseButtonClick();
                break;
        }
    }
    
    public void OnCloseButtonClick() {
        // Restart timer
        Time.timeScale = 1.0f;
        
        currentEnabledGameObj.SetActive(false);
        currentEnabledGameObj = null;
        LayoutButtons.SetActive(true);
        BackgroundGeneralPanel.SetActive(false);
        
        //Play Close sound
        SoundFXManager.instance.PlaySoundFXClip(CloseMenuSound, transform,1f);
        
        player.GetComponent<OpenInventoryScript>().ClosedInventory();
    }
    
    private void ChangeMenuScreen(GameObject objToEnable) {
        if(currentEnabledGameObj != null) { currentEnabledGameObj.SetActive(false); }
        currentEnabledGameObj = objToEnable;
        objToEnable.SetActive(true);
        
        SoundFXManager.instance.PlaySoundFXClip(OpenMenuSound, transform,1f);
    }
    
    public void OnInventoryButtonClick() { ChangeMenuScreen(InventoryPanel); }
    
    public void OnOptionsButtonClick() { ChangeMenuScreen(OptionsPanel); }
    
    public void OnStatisticsButtonClick() { ChangeMenuScreen(StatisticsPanel); }
    
    public void OnMapButtonClick() { ChangeMenuScreen(MapPanel); }
    
    public void OnTutorialButtonClick() { ChangeMenuScreen(TutorialPanel); }
    
    public void OnOpenMenuClick() { GetKeyPressed("escape"); }
    
    public void OnOpenTutorialClick() { GetKeyPressed("t"); }
}
