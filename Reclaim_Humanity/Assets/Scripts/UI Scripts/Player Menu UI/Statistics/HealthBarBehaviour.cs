using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour {
    
    [SerializeField] private Image greenBarImage;
    private const float minValue = 0.214f;
    private const float maxValue = 1.0f;

    private void Start() { ShowHealth(); }

    private void OnEnable() {
        try { ShowHealth(); }
        catch (Exception) { Debug.Log("error"); }
    }

    private void ShowHealth() {
        var maxHP = 13.0f;
        var currentHp = GameManager.partyHps[0] == 1000 ? maxHP : GameManager.partyHps[0];
        greenBarImage.fillAmount = Mathf.Lerp(minValue, maxValue, (float) GameManager.partyHps[0] / maxHP);
    }
}