using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesCanvasHandler : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    private PlayerMovement movement;
    
    [SerializeField] private GameObject recipeHandler;
    private RecipesSelectionHandler handler;

    private void Start() {
        movement = player.GetComponent<PlayerMovement>();
        handler = recipeHandler.GetComponent<RecipesSelectionHandler>();
    }

    private void OnEnable() {
        gameObject.transform.position = player.transform.position;
        if(movement != null) { movement.CurrentSpeed = 0.0f; }
    }

    private void OnDisable() {
        if (movement != null) { movement.CurrentSpeed = movement.NormalSpeed; }
        if (handler != null) { handler.ResetRecipeSlots(); }
    }

    public void OnCloseButtonClick() { player.GetComponent<OpenInventoryScript>().OpenCloseUIFunc(true); }
}
