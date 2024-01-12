using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergySO", menuName = "ScriptableObjects/EnergySpheresSO")]
public class LabEnergySO : ScriptableObject {
    // [SerializeField] private float startingEnergy = 0.0f;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float maxEnergyLab = 1000.0f; // TODO Should be initialized via .json file with all parameters when load;

    public void SetCurrentEnergy(float energy) { currentEnergy = Mathf.Clamp(energy, 0.0f, maxEnergyLab); }
    public void AddToCurrentEnergy(float energy) { currentEnergy = Mathf.Clamp(currentEnergy + energy, 0.0f, maxEnergyLab); }
    public float GetCurrentEnergy() { return currentEnergy; }
    
    
    public float GetMaximumEnergy() { return maxEnergyLab; }
}