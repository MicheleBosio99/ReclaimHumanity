using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegenerateHealthNoText : MonoBehaviour {
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float sleepTime;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AudioClip Regeneration;
    
    
    private OpenInventoryScript invScript;
    
    //private int timesSleep;
    private bool finished;

    private void Awake() {
        //timesSleep = 0;
        invScript = player.GetComponent<OpenInventoryScript>();
        gameObject.SetActive(false);
        ChangeColorImage(0.0f);
        ChangeText("");
        
        invScript.Finished = true;
    }

    private void OnEnable()
    {
        StartCoroutine(SleepCoroutine());
        
        //start sound
        SoundFXManager.instance.PlaySoundFXClip(Regeneration, transform,1f);
    }

    private void OnDisable() {
        ChangeColorImage(0.0f);
        StopAllCoroutines();
    }

    private IEnumerator SleepCoroutine()
    {
        GameManager.RestoreHps();
        invScript.Finished = false;
        var startColor = backgroundImage.color;
        var targetColor = backgroundImage.color; targetColor.a = 1.0f;

        var currentTime = 0f;
        while (currentTime < sleepTime) {
            currentTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, targetColor, Mathf.Lerp(0, 1, currentTime / sleepTime));
            yield return null;
        }
        ChangeText("Your health has been restored.\nYou can continue your mission now.");
        
        backgroundImage.color = targetColor;
        
        yield return new WaitForSeconds(2.0f);
        
        RegenerateHealth();
        
        ChangeText("");
        currentTime = 0f;
        while (currentTime < sleepTime / 2.0f) {
            currentTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(targetColor, startColor, Mathf.Lerp(0, 1, currentTime / sleepTime));
            yield return null;
        }
        backgroundImage.color = startColor;
        
        invScript.Finished = true;
        invScript.BlockPlayer(false);
        invScript.isActive = false;
        gameObject.SetActive(false);
    }

    private void ChangeColorImage(float alpha) { var color = backgroundImage.color; color.a = alpha; backgroundImage.color = color; }
    
    private void ChangeText(string _text) { text.text = _text; }
    
    private void RegenerateHealth() { GameManager.RestoreHps(); }
}
