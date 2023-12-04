using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RegenerateHealthNoText : MonoBehaviour {
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float sleepTime;
    [SerializeField] private GameObject player;
    
    private OpenInventoryScript invScript;
    
    private int timesSleep = 0;
    private bool finished;

    private void Start() {
        invScript = player.GetComponent<OpenInventoryScript>();
        gameObject.SetActive(false);
        ChangeColorImage(0.0f);
        invScript.Finished = true;
    }

    private void OnEnable() { if (timesSleep > 0) { StartCoroutine(SleepCoroutine()); } timesSleep ++; }

    private void OnDisable() { ChangeColorImage(0.0f); }

    private IEnumerator SleepCoroutine() {
        invScript.Finished = false;
        var startColor = backgroundImage.color;
        var targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        var currentTime = 0f;

        while (currentTime < sleepTime) {
            currentTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, targetColor, currentTime / sleepTime);
            
            yield return null;
        }
        
        backgroundImage.color = targetColor;
        yield return new WaitForSeconds(1.0f);
        
        RegenerateHealth();
        
        targetColor = startColor;
        startColor = backgroundImage.color;
        
        currentTime = 0f;
        
        while (currentTime < sleepTime) {
            currentTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, targetColor, currentTime / sleepTime);
            
            yield return null;
        }
        
        backgroundImage.color = targetColor;
        
        invScript.Finished = true;
        gameObject.SetActive(false);
    }

    private void RegenerateHealth() {  }

    private void ChangeColorImage(float alpha) { var color = backgroundImage.color; color.a = alpha; backgroundImage.color = color; }
}
