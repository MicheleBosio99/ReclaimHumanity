using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject blackBackground;
    [SerializeField] private TextMeshProUGUI messageText;
    
    private const float sleepTime = 2.0f;

    private void Awake() {
        if (GameManager.gameEnded) { Destroy(gameObject); }
        gameObject.SetActive(false);
        blackBackground.SetActive(false);
    }

    private void OnEnable() { StartCoroutine(EndGameRoutine()); }

    private void OnDisable() { StopAllCoroutines(); }

    private IEnumerator EndGameRoutine() {
        
        blackBackground.SetActive(true);
        
        var startColor = backgroundImage.color;
        var targetColor = backgroundImage.color; targetColor.a = 1.0f;

        var currentTime = 0f;
        while (currentTime < sleepTime) {
            currentTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, targetColor, Mathf.Lerp(0, 1, currentTime / sleepTime));
            yield return null;
        }
        
        messageText.text = "You did it! You finished the game! Congratulations!";
        yield return new WaitForSeconds(3.5f);
        messageText.text = "Still, you can visit what you left unexplored of this fantastic world.\nNow, thanks to you, everything is regaining its long time lost life.";
        yield return new WaitForSeconds(3.5f);
        messageText.text = "Thanks for playing, that really means a lot to us!\nAll progress have been saved.";
        yield return new WaitForSeconds(3.5f);
        messageText.text = "";
        
        GameManager.SaveGame();
        GameManager.gameEnded = true;
        Destroy(gameObject);
    }
}
