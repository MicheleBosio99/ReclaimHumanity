using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseWhereToGoScript : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private bool stopPlayer;

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
        stopPlayer = true;
    }
    private void OnDisable() { stopPlayer = false; }

    private void Update() {
        if (stopPlayer) { player.SetActive(false); }
        if (Input.GetKeyDown(KeyCode.F)) { OnExitButtonClick(); }
    }

    public void OnExitButtonClick() {
        player.SetActive(true);
        gameObject.SetActive(false);
    }
    
    // GTF = Go To Forest
    public void OnGTFButtonClick() { SceneManager.LoadScene(sceneName: "OvergrownForest"); }
    
    // GTC = Go To City
    public void OnGTCButtonClick() { SceneManager.LoadScene(sceneName: "RuinedCity"); }
    
    // GTW = Go To Wastelands
    public void OnGTWButtonClick() { SceneManager.LoadScene(sceneName: "Wastelands"); }
    
    
}
