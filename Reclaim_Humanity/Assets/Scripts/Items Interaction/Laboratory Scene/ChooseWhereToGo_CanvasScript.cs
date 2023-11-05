using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseWhereToGoScript : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private bool stopPlayer;
    private PlayerMovement playerMovement;

    private void Awake() {
        playerMovement = player.GetComponent<PlayerMovement>();
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
        playerMovement.CurrentSpeed = 0.0f;
    }
    private void OnDisable() { playerMovement.CurrentSpeed = playerMovement.NormalSpeed; }

    private void Update() {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.F)) { OnCloseButtonClick(); } // Must be changed to use InputActions 
    }

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
