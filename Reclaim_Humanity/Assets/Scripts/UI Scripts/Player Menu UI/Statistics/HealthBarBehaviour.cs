using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour {
    
    [SerializeField] private Image greenBarImage;
    private const float minValue = 0.214f;
    private const float maxValue = 1.0f;
    
    private int indexInParty = 0;

    private void Start() { ShowHealth(); }

    private void OnEnable() {
        try { ShowHealth(); }
        catch (Exception) { Debug.Log("error"); }
    }

    public void SetIndexInParty(int index) { indexInParty = index; }

    private void ShowHealth() {
        var maxHP = GameManager.party[indexInParty].MaxHp;
        greenBarImage.fillAmount = Mathf.Lerp(minValue, maxValue, (float) GameManager.partyHps[indexInParty] / maxHP);
    }
}