using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScenePosition : MonoBehaviour {
    
    [SerializeField] private Vector2 labInitPosition = Vector2.zero;
    [SerializeField] private Vector2 forestInitPosition = Vector2.zero;
    [SerializeField] private Vector2 cityInitPosition = Vector2.zero;
    [SerializeField] private Vector2 wastelandsInitPosition = Vector2.zero;
    
    private string labName = "Laboratory";
    private string forestName = "OvergrownForest";
    private string cityName = "RuinedCity";
    private string wastelandsName = "Wastelands";
    
    void Start() {
        var scene = SceneManager.GetActiveScene().name;
        switch (scene) {
            case "Laboratory" : gameObject.transform.position = labInitPosition; break;
            case "OvergrownForest" : gameObject.transform.position = forestInitPosition; break;
            case "RuinedCity" : gameObject.transform.position = cityInitPosition; break;
            case "Wastelands" : gameObject.transform.position = wastelandsInitPosition; break;
            default: Debug.Log("Error in finding scene name"); break;
        }
    }
}