using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollAutomatically : MonoBehaviour {
    
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float scrollSpeed = 0.02f;
    private const float targetScrollValue = 0.0f;

    private void Start() { StartCoroutine(AutoScrollCoroutine()); }

    private IEnumerator AutoScrollCoroutine() {
        yield return new WaitForSeconds(10.0f);
        while (scrollbar.value > targetScrollValue) { scrollbar.value -= Time.deltaTime * scrollSpeed; yield return null; }
        yield return new WaitForSeconds(18.0f);
        
        ChangeSceneToLab();
    }
    
    private void ChangeSceneToLab() { SceneManager.LoadScene("Laboratory"); }
}
