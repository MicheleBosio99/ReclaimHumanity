using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BaseColor : MonoBehaviour {
    
    private float distance;
    private Vector2 myPosition;
    [SerializeField] private GameObject shadowTop;
    [SerializeField] private GameObject lightTop;
    [SerializeField] private GameObject lightBottom;
    [SerializeField] private GameObject player;
    
    private SpriteRenderer _rendererCircle;
    private SpriteRenderer rendererShadowTop;
    private SpriteRenderer rendererLightTop;
    private SpriteRenderer rendererLightBottom;

    private void Start() {
        _rendererCircle = GetComponent<SpriteRenderer>();
        rendererShadowTop = shadowTop.GetComponent<SpriteRenderer>();
        rendererLightTop = lightTop.GetComponent<SpriteRenderer>();
        rendererLightBottom = lightBottom.GetComponent<SpriteRenderer>();
        
        myPosition = GetComponent<Transform>().position;
        _rendererCircle.color = Color.HSVToRGB(0.0f, 0.0f, 0.0f);
        rendererShadowTop.color = Color.HSVToRGB(0.0f, 0.0f, 0.0f);
        rendererLightBottom.color = Color.HSVToRGB(0.0f, 0.0f, 0.0f);
        rendererLightTop.color = Color.HSVToRGB(190.0f / 358.0f, 0.0f, 0.3f);
    }

    // Update is called once per frame
    void Update() {
        distance = Vector2.Distance(myPosition, player.transform.position);
        if (distance <= 6.0f) {
            ChangeColor(distance);
        }
    }

    private void ChangeColor(float _distance) {
        float intensity = 1 - (_distance / 6.0f);
        _rendererCircle.color = Color.HSVToRGB(190.0f/358.0f, 1.0f, intensity);
        rendererShadowTop.color = Color.HSVToRGB(190.0f/358.0f, 1.0f, intensity / 2);
        rendererLightBottom.color = Color.HSVToRGB(190.0f/358.0f, 0.25f, intensity);
        rendererLightTop.color = Color.HSVToRGB(190.0f/358.0f, intensity * 0.3f, 0.3f + intensity * 0.7f);
    }
}
