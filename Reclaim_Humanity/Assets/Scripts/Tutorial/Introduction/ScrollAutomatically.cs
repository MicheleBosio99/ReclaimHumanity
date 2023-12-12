using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollAutomatically : MonoBehaviour {
    
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float scrollSpeed = 0.02f;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private float initialLoadingWait;
    [SerializeField] private float sceneLoadWait;
    private const float targetScrollValue = 0.0f;

    private void Start() {
        loadingText.SetActive(true);
        continueButton.SetActive(false);
        StartCoroutine(AutoScrollCoroutine());
    }

    private IEnumerator AutoScrollCoroutine() {
        yield return new WaitForSeconds(initialLoadingWait);
        // while (scrollbar.value > targetScrollValue) { scrollbar.value -= Time.deltaTime * scrollSpeed; yield return null; }
        
        loadingText.SetActive(false);
        continueButton.SetActive(true);
        
        yield return new WaitForSeconds(sceneLoadWait);
        ChangeSceneTo("Laboratory");
    }

    public void ContinueStartGame(string sceneName) { StopAllCoroutines(); ChangeSceneTo(sceneName); }
    
    private void ChangeSceneTo(string sceneName) { SceneManager.LoadScene(sceneName); }
}
