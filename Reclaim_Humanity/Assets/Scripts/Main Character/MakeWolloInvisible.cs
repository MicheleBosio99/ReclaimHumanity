using UnityEngine;

public class MakeWolloInvisible : MonoBehaviour {
    
    [SerializeField] private GameObject mainCharacter;
    private SpriteRenderer mainCharSpriteRenderer;
    private int oldOrder = 0;

    private void Awake() { mainCharSpriteRenderer = mainCharacter.GetComponent<SpriteRenderer>(); }

    private void OnTriggerEnter2D(Collider2D other) {
        oldOrder = mainCharSpriteRenderer.sortingOrder;
        mainCharSpriteRenderer.sortingOrder = -100;
    }

    private void OnTriggerExit2D(Collider2D other) { mainCharSpriteRenderer.sortingOrder = oldOrder; }
}
