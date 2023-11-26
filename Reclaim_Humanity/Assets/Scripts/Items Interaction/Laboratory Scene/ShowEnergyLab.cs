using TMPro;
using UnityEngine;

public class ShowEnergyLab : MonoBehaviour {
    
    [SerializeField] private LabEnergySO labEnergy;
    private TextMeshProUGUI textEnergy;
    
    void Start() { textEnergy = GetComponent<TextMeshProUGUI>(); textEnergy.text = labEnergy.CurrentEnergy.ToString(); }

    public void UpdateEnergy(string energy) { textEnergy.text = energy; }
}
