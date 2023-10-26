using UnityEngine;

public class ShowEnergyLab : MonoBehaviour {
    // private bool isTriggered = false;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Awake() {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // isTriggered = true;
        _renderer.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        // isTriggered = false;
        _renderer.enabled = false;
    }
}