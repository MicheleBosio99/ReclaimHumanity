using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenePosition : MonoBehaviour {
    
    [SerializeField] private Vector2 labInitPosition = Vector2.zero;
    [SerializeField] private Vector2 forestInitPosition = Vector2.zero;
    [SerializeField] private Vector2 cityInitPosition = Vector2.zero;
    [SerializeField] private Vector2 wastelandsInitPosition = Vector2.zero;
    
    void Start() {
        if (GameManager.previousPosition != Vector3.zero) {
            gameObject.transform.position = GameManager.previousPosition;
            GameManager.previousPosition = Vector3.zero;
        }
        else {
            var scene = SceneManager.GetActiveScene().name;
            switch (scene)
            {
                case "Laboratory":
                    gameObject.transform.position = labInitPosition;
                    break;
                case "OvergrownForest":
                    gameObject.transform.position = forestInitPosition;
                    break;
                case "RuinedCity":
                    gameObject.transform.position = cityInitPosition;
                    break;
                case "Wastelands":
                    gameObject.transform.position = wastelandsInitPosition;
                    break;
                default:
                    Debug.Log("Error in finding scene name");
                    break;
            }
        }
        
        
        // gameObject.GetComponent<OpenInventoryScript>().enabled = true;
    }
}
