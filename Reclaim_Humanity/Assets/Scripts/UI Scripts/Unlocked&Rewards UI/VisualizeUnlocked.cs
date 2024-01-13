using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisualizeUnlocked : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI unlockedText;
    [SerializeField] private GameObject unlockedPanel;
    [SerializeField] private AudioClip RecipeUnlock;
    
    private Queue<string> powerUpsMessagesQueue;
    
    private bool hasCoroutineDone = true;

    private void Awake() {
        unlockedPanel.SetActive(false); unlockedText.text = "";
        powerUpsMessagesQueue = new Queue<string>();
    }
    
    public void StartShowUnlockedRecipeMessage(string type, string reward) { StartCoroutine(ShowUnlockedRecipeMessage(type, reward)); }

    public void StartShowUnlockedPowerUpMessage(string message) {
        powerUpsMessagesQueue.Enqueue(message);
        if(hasCoroutineDone) { StartCoroutine(ShowUnlockedPowerUpMessage()); }
    }

    private IEnumerator ShowUnlockedRecipeMessage(string type, string reward) {

		unlockedPanel.SetActive(true);
        var rewardMessage = $"Congratulations! You unlocked the new {type}: <u>{reward}</u>";
        
        //Play unlock sound
        SoundFXManager.instance.PlaySoundFXClip(RecipeUnlock, transform,1f);
        
        foreach (var letter in rewardMessage) { unlockedText.text += letter; }
        
        yield return new WaitForSeconds(5.0f);
        
        unlockedPanel.SetActive(false);
        unlockedText.text = "";
    }
    
    private IEnumerator ShowUnlockedPowerUpMessage() {
        hasCoroutineDone = false;
        while (powerUpsMessagesQueue.Count > 0) {
            var message = powerUpsMessagesQueue.Dequeue();
            
            unlockedPanel.SetActive(true);
        
            //Play unlock sound
            SoundFXManager.instance.PlaySoundFXClip(RecipeUnlock, transform,1f);
        
            unlockedText.text = message;
        
            yield return new WaitForSeconds(powerUpsMessagesQueue.Count == 0 ? 5.0f : 3.0f);
            
            unlockedText.text = "";
        }
        
        unlockedPanel.SetActive(false);
        unlockedText.text = "";
        hasCoroutineDone = true;
    }
    
}
