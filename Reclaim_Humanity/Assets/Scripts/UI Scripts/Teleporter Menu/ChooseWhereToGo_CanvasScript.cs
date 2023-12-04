using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseWhereToGoScript : MonoBehaviour {
    
    [SerializeField] private GameObject player;

    private void OnEnable() { gameObject.transform.position = player.transform.position; }

    // private void OnEnable() {
    //     gameObject.transform.position = player.transform.position;
    //     if(movement != null) { movement.CurrentSpeed = 0.0f; }
    // }
    //
    // private void OnDisable() { if (movement != null) { movement.CurrentSpeed = movement.NormalSpeed; } }

    public void OnCloseButtonClick() { gameObject.SetActive(false); }
    
    // GTF = Go To Forest
    public void OnGTFButtonClick() { SceneManager.LoadScene(sceneName: "OvergrownForest"); }
    
    // GTC = Go To City
    public void OnGTCButtonClick() { SceneManager.LoadScene(sceneName: "RuinedCity"); }
    
    // GTW = Go To Wastelands
    public void OnGTWButtonClick() { SceneManager.LoadScene(sceneName: "Wastelands"); }
    
    // GTL = Go To Laboratory
    public void OnGTLButtonClick() { SceneManager.LoadScene(sceneName: "Laboratory"); }
}