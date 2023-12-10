using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollAutomatically : MonoBehaviour {
    
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float scrollSpeed = 0.02f;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject loadingText;
    private const float targetScrollValue = 0.0f;

    private void Start() {
        loadingText.SetActive(true);
        continueButton.SetActive(false);
        StartCoroutine(AutoScrollCoroutine());
    }

    private IEnumerator AutoScrollCoroutine() {
        yield return new WaitForSeconds(10.0f);
        while (scrollbar.value > targetScrollValue) { scrollbar.value -= Time.deltaTime * scrollSpeed; yield return null; }
        
        yield return new WaitForSeconds(5.0f);
        
        loadingText.SetActive(false);
        continueButton.SetActive(true);
        
        yield return new WaitForSeconds(25.0f);
        
        ChangeSceneTo("Laboratory");
    }

    public void ContinueStartGame(string sceneName) { StopAllCoroutines(); ChangeSceneTo(sceneName); }
    
    private void ChangeSceneTo(string sceneName) { SceneManager.LoadScene(sceneName); }
}
