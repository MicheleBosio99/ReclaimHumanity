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

    private void OnDisable() {
        powerUpsMessagesQueue = new Queue<string>();
        StopAllCoroutines();
        hasCoroutineDone = true;
        unlockedPanel.SetActive(false);
    }

    public void StartShowUnlockedRecipeMessage(string message) {
        unlockedPanel.SetActive(true);
        StartCoroutine(ShowUnlockedRecipeMessage(message));
    }

    public void StartShowUnlockedPowerUpMessage(string message) {
        powerUpsMessagesQueue.Enqueue(message);
        if(hasCoroutineDone) { StartCoroutine(ShowUnlockedPowerUpMessage()); }
    }

    private IEnumerator ShowUnlockedRecipeMessage(string message) {

		unlockedPanel.SetActive(true);

        //Play unlock sound
        if(SoundFXManager.instance != null) { SoundFXManager.instance.PlaySoundFXClip(RecipeUnlock, transform,1f); }
        
        unlockedText.text = message;
        
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
            if(SoundFXManager.instance != null) SoundFXManager.instance.PlaySoundFXClip(RecipeUnlock, transform,1f);
        
            unlockedText.text = message;
        
            yield return new WaitForSeconds(powerUpsMessagesQueue.Count == 0 ? 5.0f : 2.0f);
            
            unlockedText.text = "";
        }
        
        unlockedPanel.SetActive(false);
        unlockedText.text = "";
        hasCoroutineDone = true;
    }
    
}
