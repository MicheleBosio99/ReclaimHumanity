using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour {
    
    [SerializeField] private GameObject unlockedANDReward;
    
    private List<PowerUp> powerUps;

    private void Awake() {
        powerUps = Resources.LoadAll<PowerUp>("PowerUps").ToList();
        powerUps.Sort((powerUp1, powerUp2) => powerUp1.energyThreshold.CompareTo(powerUp2.energyThreshold));
        UpdatePowerUps();
    }

    public void UpdatePowerUps() {
        foreach (var powUp in powerUps.Where(powUp =>
                     (GameManager.energyInLab >= powUp.energyThreshold) && powUp.enabled == false)) {
            powUp.CallSelectedFunction();
            ShowPopupMessage(powUp.powerUpText);
        }
    }

    private void ShowPopupMessage(string message) {
        unlockedANDReward.GetComponent<VisualizeUnlocked>().StartShowUnlockedPowerUpMessage(message);
    }
}
