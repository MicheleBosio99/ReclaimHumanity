using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseWhereToGoScript : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private PlayerMovement movement;

    private void Start() { movement = player.GetComponent<PlayerMovement>(); }

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
        if(movement != null) { movement.CurrentSpeed = 0.0f; }
    }

    private void OnDisable() { if (movement != null) { movement.CurrentSpeed = movement.NormalSpeed; } }

    public void OnCloseButtonClick() { gameObject.SetActive(false); }
    
    // GTF = Go To Forest
    public void OnGTFButtonClick() {
        Debug.Log("pressed");
        SceneManager.LoadScene(sceneName: "OvergrownForest");
    }
    
    // GTC = Go To City
    public void OnGTCButtonClick() { SceneManager.LoadScene(sceneName: "RuinedCity"); }
    
    // GTW = Go To Wastelands
    public void OnGTWButtonClick() { SceneManager.LoadScene(sceneName: "Wastelands"); }
}
