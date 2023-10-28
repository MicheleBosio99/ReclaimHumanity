using UnityEngine;

public class RecipeSceneTrigger : MonoBehaviour {
    [SerializeField] private int sceneBuildIndex = 2;
    private bool isTriggered = false;

    private SpriteRenderer _renderer;
    [SerializeField] private GameObject FButton;

    [SerializeField] private GameObject generateEnergy;
    
    private void Start() {
        _renderer = FButton.GetComponent<SpriteRenderer>();
        _renderer.enabled = false;
        generateEnergy.SetActive(false);
    }


    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        isTriggered = true;
        _renderer.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isTriggered = false;
        _renderer.enabled = false;
    }

    private void Update() {
        if (isTriggered && Input.GetKeyDown(KeyCode.F)) {
            generateEnergy.SetActive(true);
        }
    }
}
