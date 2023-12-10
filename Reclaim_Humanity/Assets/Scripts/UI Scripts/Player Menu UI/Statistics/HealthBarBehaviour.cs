using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour {
    
    [SerializeField] private Image greenBarImage;
    private const float minValue = 0.214f;
    private const float maxValue = 1.0f;
    
    private const float maxHealth = 125.0f;

    private void Start() {
        greenBarImage.fillAmount = Mathf.Lerp(minValue, maxValue, GameManager.party[0].MaxHp / maxHealth);
    }
}
