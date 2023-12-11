using TMPro;
using UnityEngine;

public class ShowEnergyLab : MonoBehaviour {
    
    [SerializeField] private GameObject labEnergySOSetter;
    private TextMeshProUGUI textEnergy;
    
    private void Start() {
        textEnergy = GetComponent<TextMeshProUGUI>();
        textEnergy.text = labEnergySOSetter.GetComponent<LabEnergySOSetter>().GetCurrentEnergy().ToString();
    }

    public void UpdateEnergy(string energy) { textEnergy.text = energy; }
}
