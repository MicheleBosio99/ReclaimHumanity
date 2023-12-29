using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualizeUnlocked : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI unlockedText;
    [SerializeField] private GameObject unlockedPanel;
    [SerializeField] private AudioClip RecipeUnlock;

    private void Awake() { unlockedPanel.SetActive(false); unlockedText.text = ""; }
    
    public void StartShowUnlockedMessage(string type, string reward) { StartCoroutine(ShowUnlockedMessage(type, reward)); }

    private IEnumerator ShowUnlockedMessage(string type, string reward) {

		unlockedPanel.SetActive(true);
        var rewardMessage = $"Congratulations! You unlocked the new {type}: <u>{reward}</u>";
        
        //Play unlock sound
        SoundFXManager.instance.PlaySoundFXClip(RecipeUnlock, transform,1f);
        
        foreach (var letter in rewardMessage) { unlockedText.text += letter; }
        
        yield return new WaitForSeconds(5.0f);
        
        unlockedPanel.SetActive(false);
        unlockedText.text = "";
    }
    
}
