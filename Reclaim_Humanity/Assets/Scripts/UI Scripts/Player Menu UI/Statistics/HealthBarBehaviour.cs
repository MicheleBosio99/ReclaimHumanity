using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour {
    
    [SerializeField] private Image greenBarImage;
    private const float minValue = 0.214f;
    private const float maxValue = 1.0f;
    
    private const float maxHealth = 125.0f;

    private void Start() {
        var clampedHealth = GameManager.party[0].MaxHp / maxHealth;
        Debug.Log(clampedHealth);
        var lerpHealth = Mathf.Lerp(minValue, maxValue, clampedHealth);
        Debug.Log(lerpHealth);
        
        greenBarImage.fillAmount = lerpHealth;
    }
}
