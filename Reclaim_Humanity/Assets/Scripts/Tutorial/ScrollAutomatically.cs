using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAutomatically : MonoBehaviour {
    
    [SerializeField] private GameObject descriptionImage;
    private ScrollRect scrollRect;
    private const float scrollSpeed = 2f;

    private void Awake() { scrollRect = descriptionImage.GetComponent<ScrollRect>(); }

    private void Start() { StartCoroutine(AutoScrollCoroutine()); }

    private IEnumerator AutoScrollCoroutine() {
        const float targetScrollPosition = 1f;

        while (!Mathf.Approximately(scrollRect.verticalNormalizedPosition, targetScrollPosition)) {
            var step = Mathf.MoveTowards(scrollRect.verticalNormalizedPosition, targetScrollPosition, scrollSpeed * Time.deltaTime);
            scrollRect.verticalNormalizedPosition = step;
            yield return null;
        }
    }
    
}
