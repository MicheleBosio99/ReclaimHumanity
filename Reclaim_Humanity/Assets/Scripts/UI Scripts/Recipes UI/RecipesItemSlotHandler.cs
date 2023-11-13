using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Used to handle components of itemSlots for recipes (the three slots that contain items in RecipeCraftingPanel)
public class RecipesItemSlotHandler : MonoBehaviour {
    
    [SerializeField] private Image recipeImage;
    [SerializeField] private TextMeshProUGUI recipeTextQuantity;

    public void SetImageWithTransparency(Sprite sprite, float transparency) {
        recipeImage.sprite = sprite;
        var color = recipeImage.color; color.a = transparency; recipeImage.color = color;
    }

    public void SetQuantityText(int quantityText) {recipeTextQuantity.text = quantityText <= 0 ? "" : quantityText.ToString(); }
    
}
