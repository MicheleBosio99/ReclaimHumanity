using System;
using UnityEngine;
using UnityEngine.UI;

public class HandleMovementOnMap : MonoBehaviour {
    
    [SerializeField] private Image mapImage;
    [SerializeField] private GameObject player;
    
    private const float imageDimension = 1200.0f;
    private Vector2 initialPosition;

    private void Start() { GoOnPosition(initialPosition); }

    private void OnEnable() { GoOnPosition(Vector2.zero); }

    private void GoOnPosition(Vector2 position) {
        // Debug.Log(mapImage.GetComponent<RectTransform>().transform);
    }

    // private Vector2 ComputeMapPosition() {
    //     return 
    // }
}
