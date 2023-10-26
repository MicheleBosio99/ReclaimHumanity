using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeSceneTrigger : MonoBehaviour {
    [SerializeField] private int sceneBuildIndex = 2;
    private bool isTriggered = false;
    
    private SpriteRenderer FButtonRenderer;
    [SerializeField] private GameObject FButtonSprite;

    private void Start() {
        FButtonRenderer = FButtonSprite.GetComponent<SpriteRenderer>();
        FButtonRenderer.enabled = false;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        isTriggered = true;
        FButtonRenderer.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isTriggered = false;
        FButtonRenderer.enabled = false;
    }

    private void Update() {
        if (isTriggered && Input.GetKeyDown(KeyCode.F)) {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
