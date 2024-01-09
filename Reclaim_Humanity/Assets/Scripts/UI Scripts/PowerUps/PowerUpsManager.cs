using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour {
    
    [SerializeField] private List<PowerUp> powerUps;
    
    private void Awake() { UpdatePowerUps(); }

    public void UpdatePowerUps() {
        foreach (var powUp in powerUps.Where(powUp => GameManager.energyInLab >= powUp.energyThreshold)) { powUp.CallSelectedFunction(); }
    }
}