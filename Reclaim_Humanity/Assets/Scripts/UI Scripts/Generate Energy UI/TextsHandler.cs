using TMPro;
using UnityEngine;

public class TextsHandler : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI energySingle;
    [SerializeField] private TextMeshProUGUI energyTotal;
    
    private const string noItemSelectedText = "Use right-click to send whole stack from inventory to burner. Doesn't still work the other way D:";
    
    public void Start() { descriptionText.text = noItemSelectedText; }

    public void UpdateEnergy(string energy) { energyTotal.text = energy; }
    
    // Probably will need some kind of format
    public void UpdateDescriptionAndEnergy(string description, string energy) {
        if (description == "") { description = noItemSelectedText; }

        descriptionText.text = description;
        energySingle.text = energy;
    }
    
}
