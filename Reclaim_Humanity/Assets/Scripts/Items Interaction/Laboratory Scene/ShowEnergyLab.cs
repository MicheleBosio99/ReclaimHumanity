using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnergyLab : MonoBehaviour {
    
    // private bool isTriggered = false;
    private TextMeshProUGUI textEnergy;
    private int energy;
    [SerializeField] private LabEnergySO labEnergy;
    [SerializeField] private Canvas canvas;
    
    
    // Start is called before the first frame update
    void Awake() {
        energy = (int) labEnergy.totalEnergy;
        textEnergy = GetComponent<TextMeshProUGUI>();
        canvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        textEnergy.text = energy.ToString();
        // isTriggered = true;
        canvas.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        // isTriggered = false;
        canvas.enabled = false;
    }
}