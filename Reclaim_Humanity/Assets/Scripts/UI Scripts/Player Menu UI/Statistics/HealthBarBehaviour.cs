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

    public void ShowHealth() {
        var maxHP = GameManager.party[indexInParty].MaxHp;
        var currentHps = GameManager.partyHps[indexInParty] == 1000 ? maxHP : GameManager.partyHps[indexInParty];
        
        greenBarImage.fillAmount = Mathf.Lerp(minValue, maxValue, (float) currentHps / maxHP);
    }
}