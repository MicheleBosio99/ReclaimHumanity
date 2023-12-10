using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour {
    
    [SerializeField] private Image greenBarImage;
    private const float minValue = 0.214f;
    private const float maxValue = 1.0f;

    private void Start() {
        var currentHp = GameManager.partyHps[0] == 1000 ? GameManager.party[0].MaxHp : GameManager.partyHps[0];
        greenBarImage.fillAmount = Mathf.Lerp(minValue, maxValue, (float) GameManager.partyHps[0] / (float) GameManager.party[0].MaxHp);
    }
}