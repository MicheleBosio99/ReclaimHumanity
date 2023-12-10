using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipesButtonsHandler : MonoBehaviour , IPointerClickHandler {
    
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private Image recipeImage;
    
    private RecipesSelectionHandler handler;
    private Recipe recipe;

    public void SetHandler(RecipesSelectionHandler _handler) { handler = _handler; }

    public void SetRecipe(Recipe _recipe) {
        recipe = _recipe;
        SetRecipeText();
        SetRecipeImage();
    }

    private void SetRecipeText() { recipeText.text = $"{recipe.recipeName}: {recipe.recipeDescription}"; }

    private void SetRecipeImage() {
        var color = recipeImage.color; color.a = 1f; recipeImage.color = color;
        recipeImage.sprite = Resources.Load<ItemsSO>($"Items/{recipe.itemResultID}").ItemSprite;
    }

    public void OnPointerClick(PointerEventData eventData) { handler.RecipeButtonClicked(recipe); }
}
