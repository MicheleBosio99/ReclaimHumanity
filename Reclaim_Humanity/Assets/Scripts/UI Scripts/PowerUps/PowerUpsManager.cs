using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour {
    
    [SerializeField] private GameObject unlockedANDReward;
    
    private List<PowerUp> powerUps;

    private void Awake() { UpdatePowerUps(); }

    public void UpdatePowerUps() {
        foreach (var powUp in GameManager.powerUps.Where(powUp =>
                     (GameManager.energyInLab >= powUp.energyThreshold) && powUp.enabled == false)) {
            powUp.enabled = true;
            powUp.CallSelectedFunction();
            ShowPopupMessage(powUp.powerUpText);
        }
    }

    private void ShowPopupMessage(string message) {
        unlockedANDReward.GetComponent<VisualizeUnlocked>().StartShowUnlockedPowerUpMessage(message);
    }
}
