using UnityEngine;

public class GenerateEnergyCanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool stopPlayer;
    private OpenInventoryScript openInventoryScript;

    private void Awake() {
        openInventoryScript = player.GetComponent<OpenInventoryScript>();
        openInventoryScript.CurrentlyOpenUI = gameObject;
    }

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
        Time.timeScale = 0.0f;
    }

    private void OnDisable() { Time.timeScale = 1.0f; }

    // ON CLOSE BUTTON CALLED WHEN F PRESSED. HANDLER IS IN PLAYER UI SCRIPT AND GETS GAMEOBJECT TO CLOSE REFERENCE FROM SCRIPTS OF EVERY CANVAS

    public void OnCloseButtonClick() { gameObject.SetActive(false); }
}
