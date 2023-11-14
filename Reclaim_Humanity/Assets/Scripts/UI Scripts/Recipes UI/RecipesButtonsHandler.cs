using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipesButtonsHandler : MonoBehaviour , IPointerClickHandler { 
    [SerializeField] private TextMeshProUGUI recipeText;
    private RecipesSelectionHandler handler;
    
    private Recipe recipe;

    public void SetHandler(RecipesSelectionHandler _handler) {this.handler = _handler; }

    public void SetRecipe(Recipe _recipe) {
        this.recipe = _recipe;
        SetRecipeText();
    }

    private void SetRecipeText() { recipeText.text = $"{recipe.recipeName}: {recipe.recipeDescription}"; }

    public void OnPointerClick(PointerEventData eventData) { handler.RecipeButtonClicked(recipe); }
}
