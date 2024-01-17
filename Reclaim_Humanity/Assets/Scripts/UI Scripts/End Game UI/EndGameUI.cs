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
        
        messageText.text = "Thank you for playing the game.\nIt means really much to us that you've completed it.";
        yield return new WaitForSeconds(4.0f);
        messageText.text = "All your files have been saved. You can continue play this save and explore the rest of the world you didn't see already.";
        yield return new WaitForSeconds(4.0f);
        messageText.text = "Mind that it could be still bugged since we didn't test the aftergame too much :) ";
        yield return new WaitForSeconds(3.0f);
        messageText.text = "Thanks again.\nGoodbye.";
        yield return new WaitForSeconds(4.0f);
        messageText.text = "";
        
        GameManager.SaveGame();
        GameManager.gameEnded = true;
        Destroy(gameObject);
    }
}
