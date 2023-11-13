using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipesButtonsHandler : MonoBehaviour , IPointerClickHandler { 
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private GameObject recipesSelectionHanlder;
    private RecipesSelectionHandler handler;
    
    private Recipe recipe;

    private void Start() { handler = recipesSelectionHanlder.GetComponent<RecipesSelectionHandler>(); }

    public void SetRecipe(Recipe _recipe) { this.recipe = _recipe; }

    private void SetRecipeText(string recipeName, string recipeDescription) { recipeText.text = $"{recipeName}: {recipeDescription}"; }

    public void OnPointerClick(PointerEventData eventData) { handler.RecipeButtonClicked(recipe); }
}
