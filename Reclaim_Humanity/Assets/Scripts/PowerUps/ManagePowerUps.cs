using System;
using System.Collections.Generic;

public class ManagePowerUp {
    
    private float currentEnergy;

    public void ChangeCurrentEnergy() {
        
    }
    
    private void ApplyPowerUps() {
        
    }
    
    private static void ChangeLifePoints(float value) { }
    
    private static void ChangeMapArea(float value) { }
    
    private static void ChangeWolloLevel(float value) { }
    
    private static void EnableBiomesTeleport(float value) { }
    
    private Dictionary<float, Action<float>> powerUps = new Dictionary<float, Action<float>>() {
        {5.0f, EnableBiomesTeleport},
        {50.0f, EnableBiomesTeleport}
    };
}

