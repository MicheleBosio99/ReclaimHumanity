using System;
using UnityEngine;

public class LabEnergySOSetter : MonoBehaviour {
    
    [SerializeField] private LabEnergySO energySO;

    private void Start() { energySO.SetCurrentEnergy(GameManager.energyInLab); }

    public void UpdateEnergySO(float energy) {
        energySO.AddToCurrentEnergy(energy);
        GameManager.energyInLab = energySO.GetCurrentEnergy();
    }

    public float GetCurrentEnergy() { return energySO.GetCurrentEnergy(); }
    
    public float GetMaxEnergy() { return energySO.GetMaximumEnergy(); }
}
