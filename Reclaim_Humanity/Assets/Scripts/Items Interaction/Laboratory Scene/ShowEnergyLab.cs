using TMPro;
using UnityEngine;

public class ShowEnergyLab : MonoBehaviour {
    
    [SerializeField] private LabEnergySO labEnergy;
    private TextMeshProUGUI textEnergy;
    
    void Awake() {
        textEnergy = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textEnergy.text = labEnergy.totalEnergy.ToString();
    }
}
