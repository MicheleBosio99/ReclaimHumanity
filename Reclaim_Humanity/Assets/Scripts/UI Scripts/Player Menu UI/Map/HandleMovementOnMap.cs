using UnityEngine;
using UnityEngine.UI;

public class HandleMovementOnMap : MonoBehaviour {
    
    [SerializeField] private Image image;
    [SerializeField] private GameObject player;
    
    private const float imageDimensions = 1200.0f;
    private Vector2 initialPosition = new Vector2(0.0f, 0.0f);
    private Vector2 currentPosition;
    

    private void Start() {
        GoOnStartPosition();
    }

    private void GoOnStartPosition() {
        
    }
}
