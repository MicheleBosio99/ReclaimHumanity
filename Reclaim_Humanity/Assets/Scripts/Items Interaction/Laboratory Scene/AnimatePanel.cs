using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimatePanel : MonoBehaviour
{
    private Image image;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float fps = 10;

    private void Awake() { image = gameObject.GetComponent<Image>(); }
    private void OnEnable() { Play(); }
    private void OnDisable() { Stop(); }

    private void Play() {
        Stop();
        StartCoroutine(AnimSequence());
    }
 
    private void Stop() {
        StopAllCoroutines();
        ShowFrame(0);
    }
 
    IEnumerator AnimSequence() {
        var delay = new WaitForSeconds(1 / fps);
        int index = 0;
        while(true) {
            if (index >= sprites.Length) index = 0;
            ShowFrame(index);
            index++;
            yield return delay;
        }
    }
 
    private void ShowFrame(int index) { image.sprite = sprites[index]; }
}
