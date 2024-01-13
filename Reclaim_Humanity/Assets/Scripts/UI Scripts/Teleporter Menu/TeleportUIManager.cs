using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportUIManager : MonoBehaviour {
    
    [SerializeField] private GameObject notEnoughEnergyPanel;
    
    [SerializeField] private GameObject overgrownForestButton;
    [SerializeField] private GameObject ruinedCityButton;
    [SerializeField] private GameObject wastelandsButton;
    
    private const int minEnergyOvergrownForest = 0;
    private const int minEnergyRuinedCity = 50;
    private const int minEnergyWastelands = 150;
    
    private GameObject forestBlock;
    private GameObject cityBlock;
    private GameObject wastelandsBlock;

    private void OnEnable() {
        var energyInLab = GameManager.energyInLab;
        if (energyInLab < minEnergyWastelands) { LockAccessToWastelands(); }
        if (energyInLab < minEnergyRuinedCity) { LockAccessToRuinedCity(); }
        if (energyInLab < minEnergyOvergrownForest) { LockAccessToOvergrownForest(); }
    }

    private void OnDisable() {
        if (forestBlock != null) { Destroy(forestBlock); }
        if (cityBlock != null) { Destroy(cityBlock); }
        if (wastelandsBlock != null) { Destroy(wastelandsBlock); }
        
        overgrownForestButton.GetComponent<Button>().interactable = true;
        ruinedCityButton.GetComponent<Button>().interactable = true;
        wastelandsButton.GetComponent<Button>().interactable = true;
    }

    private void LockAccessToOvergrownForest() {
        overgrownForestButton.GetComponent<Button>().interactable = false;
        forestBlock = Instantiate(notEnoughEnergyPanel, overgrownForestButton.transform, false);
        forestBlock.transform.localPosition = Vector3.zero;
        forestBlock.transform.Find("BackgroundNEEPanel")
            .transform.Find("EnergyRequiredText")
            .GetComponent<TextMeshProUGUI>().text = minEnergyOvergrownForest.ToString();
    }
    
    private void LockAccessToRuinedCity() {
        ruinedCityButton.GetComponent<Button>().interactable = false;
        cityBlock = Instantiate(notEnoughEnergyPanel, ruinedCityButton.transform, false);
        cityBlock.transform.localPosition = Vector3.zero;
        cityBlock.transform.Find("BackgroundNEEPanel")
            .transform.Find("EnergyRequiredText")
            .GetComponent<TextMeshProUGUI>().text = minEnergyRuinedCity.ToString();
        
    }
    
    private void LockAccessToWastelands() {
        wastelandsButton.GetComponent<Button>().interactable = false;
        wastelandsBlock = Instantiate(notEnoughEnergyPanel, wastelandsButton.transform, false);
        wastelandsBlock.transform.localPosition = Vector3.zero;
        wastelandsBlock.transform.Find("BackgroundNEEPanel")
            .transform.Find("EnergyRequiredText")
            .GetComponent<TextMeshProUGUI>().text = minEnergyWastelands.ToString();
    }
}
