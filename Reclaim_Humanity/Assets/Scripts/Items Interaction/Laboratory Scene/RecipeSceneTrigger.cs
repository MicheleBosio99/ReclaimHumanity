using UnityEngine;
using UnityEngine.Serialization;

public class RecipeSceneTrigger : MonoBehaviour {
    
    private bool isTriggered = false;
    private SpriteRenderer FButtonRenderer;
    [SerializeField] private GameObject FButton;
    //[SerializeField] private GameObject ToOpenUI;
    
    private void Start() {
        FButtonRenderer = FButton.GetComponent<SpriteRenderer>();
        FButtonRenderer.enabled = false;
        //ToOpenUI.SetActive(false);
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
            //ToOpenUI.SetActive(true);
        }
    }
}
