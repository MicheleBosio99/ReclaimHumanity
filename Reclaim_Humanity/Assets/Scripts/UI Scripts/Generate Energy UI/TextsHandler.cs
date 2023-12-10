using TMPro;
using UnityEngine;

public class TextsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI energySingle;
    [SerializeField] private TextMeshProUGUI energyTotal;
    
    public void UpdateEnergy(string energy) { energyTotal.text = energy; }
    
    // Probably will need some kind of format
    public void UpdateDescriptionAndEnergy(string description, string energy) {
        descriptionText.text = description;
        energySingle.text = energy;
    }
    
}
