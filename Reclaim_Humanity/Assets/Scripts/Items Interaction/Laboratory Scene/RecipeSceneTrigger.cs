using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeSceneTrigger : MonoBehaviour {
    [SerializeField] private int sceneBuildIndex = 0;
    private bool isTriggered = false;
    private SpriteRenderer FButtonRenderer;
    [SerializeField] private Sprite FButton;

    private void Start() {
        FButtonRenderer = FButton.GetComponent<SpriteRenderer>();
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
            print("Switching Scene");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
