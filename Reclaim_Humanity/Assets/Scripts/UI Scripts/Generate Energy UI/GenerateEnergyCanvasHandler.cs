using UnityEngine;

public class GenerateEnergyCanvasHandler : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private PlayerMovement movement;
    
    private void Start() {
        movement = player.GetComponent<PlayerMovement>();
    }

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
    }

    public void OnCloseButtonClick() { player.GetComponent<OpenInventoryScript>().OpenCloseUIFunc(true); }
}
