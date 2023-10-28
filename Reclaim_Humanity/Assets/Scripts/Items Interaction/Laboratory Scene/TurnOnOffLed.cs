using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffLed : MonoBehaviour {
    [SerializeField] private Sprite ledOff;
    [SerializeField] private Sprite ledOn;

    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = ledOff;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") { _renderer.sprite = ledOn; }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") { _renderer.sprite = ledOff; }
    }
}
