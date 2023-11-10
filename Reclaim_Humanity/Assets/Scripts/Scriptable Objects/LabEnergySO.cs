using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnergySO", menuName = "ScriptableObjects/EnergySpheresSO")]
public class LabEnergySO : ScriptableObject {
    [SerializeField] private float startingEnergy = 280.0f;
    private float currentEnergy = 0.0f;
    [SerializeField] private float maxEnergyLab = 500.0f; // Should be initialized via .json file with all parameters;
    
    private void Awake() { currentEnergy = startingEnergy; }
    
    public float CurrentEnergy {
        get => currentEnergy;
        set => currentEnergy = value;
    }

    public float MaxEnergyLab {
        get => maxEnergyLab;
        set => maxEnergyLab = value;
    }
}