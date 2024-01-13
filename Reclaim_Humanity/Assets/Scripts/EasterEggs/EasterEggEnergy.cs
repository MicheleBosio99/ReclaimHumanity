using UnityEngine;

public class EasterEggEnergy : MonoBehaviour {
    
    private const string desiredCombination = "bosio";
    private string currentInput = "";
    
    private void Update() {
        if (GameManager.energyInLab >= 50) { Destroy(gameObject); }
        if (!Input.anyKeyDown) return;
            
        if (!desiredCombination.Contains(Input.inputString)) { currentInput = ""; return; }
        currentInput += Input.inputString;

        if (!currentInput.Equals(desiredCombination, System.StringComparison.OrdinalIgnoreCase)) return;
        
        ActivateEasterEgg();
        currentInput = "";
    }

    private void ActivateEasterEgg() { GameManager.energyInLab += 50; }
}