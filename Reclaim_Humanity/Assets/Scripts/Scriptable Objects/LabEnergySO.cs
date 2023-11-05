using UnityEngine;

[CreateAssetMenu(fileName = "EnergySO", menuName = "ScriptableObjects/EnergySpheresSO")]
public class LabEnergySO : ScriptableObject {
    [SerializeField] private float currentEnergy = 0.0f;
    [SerializeField] private float maxEnergyLab = 500.0f; // Should be initialized via .json file with all parameters;

    public float CurrentEnergy {
        get => currentEnergy;
        set => currentEnergy = value;
    }

    public float MaxEnergyLab {
        get => maxEnergyLab;
        set => maxEnergyLab = value;
    }
}