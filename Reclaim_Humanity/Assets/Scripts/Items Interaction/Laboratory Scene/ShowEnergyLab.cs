using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowEnergyLab : MonoBehaviour {
    
    // private bool isTriggered = false;
    private int energy;
    [SerializeField] private LabEnergySO labEnergy;
    [SerializeField] private TextMeshProUGUI textEnergy;
    private Canvas canvas;
    
    // Start is called before the first frame update
    void Awake() {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Update() { energy = (int) labEnergy.totalEnergy; }

    private void OnTriggerEnter2D(Collider2D other) {
        textEnergy.text = energy.ToString();
        canvas.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canvas.enabled = false;
    }
}