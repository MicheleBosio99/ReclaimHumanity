using System.Collections;
using UnityEngine;

public class LoadOnlyFirstTime : MonoBehaviour {
    
    private const string tutorialKey = "IsFirstTimePlayingEver";
    
    private void Awake() {
        if (PlayerPrefs.HasKey(tutorialKey)) {
            if (PlayerPrefs.GetInt(tutorialKey) == 1) { Destroy(gameObject); }
            else {  }
        }
        StartCoroutine(ShowTutorial());
        PlayerPrefs.SetInt(tutorialKey, 1);
    }
    
    [SerializeField] private GameObject tutorialGuideUI;

    public void OnCloseButtonClick() {  }
    public void OnGoOnButtonClick() {  }

    private IEnumerator ShowTutorial() {
        yield return null;
    }
}
