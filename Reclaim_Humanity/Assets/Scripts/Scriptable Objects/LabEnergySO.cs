using UnityEngine;

[CreateAssetMenu(fileName = "EnergyScriptableObject", menuName = "ScriptableObjects/EnergySpheresSO")]
public class LabEnergySO : ScriptableObject {
    [SerializeField] private float currentEnergy = 0.0f;
    [SerializeField] private float maxEnergyLab = 500.0f;

    public float CurrentEnergy {
        get => currentEnergy;
        set => currentEnergy = value;
    }

    public float MaxEnergyLab {
        get => maxEnergyLab;
        set => maxEnergyLab = value;
    }
}