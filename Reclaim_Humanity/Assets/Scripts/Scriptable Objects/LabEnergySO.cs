using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergySO", menuName = "ScriptableObjects/EnergySpheresSO")]
public class LabEnergySO : ScriptableObject {
    // [SerializeField] private float startingEnergy = 0.0f;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float maxEnergyLab = 1000.0f; // TODO Should be initialized via .json file with all parameters when load;

    public float CurrentEnergy {
        get => currentEnergy;
        set => currentEnergy = value;
    }

    public float MaxEnergyLab {
        get => maxEnergyLab;
        set => maxEnergyLab = value;
    }
    
    // public float GetCurrentEnergy() { return currentEnergy; }
    //
    // // public void SetCurrentEnergy(float energy) { currentEnergy = energy; }
    //
    // public void AddEnergyToCurrent(float energy) {
    //     if(currentEnergy + energy < maxEnergyLab) { currentEnergy += startingEnergy; }
    //     else { currentEnergy = maxEnergyLab; }
    //     Debug.Log(energy + ", " + currentEnergy);
    // }
    //
    // public float GetMaxEnergy() { return maxEnergyLab; }
}