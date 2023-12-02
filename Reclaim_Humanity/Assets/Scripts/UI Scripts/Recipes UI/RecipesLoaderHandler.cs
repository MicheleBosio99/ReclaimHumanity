using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipesLoaderHandler : MonoBehaviour {
    
    [SerializeField] private GameObject handlerGO;
    private RecipesSelectionHandler handler;
    [SerializeField] private TextAsset recipesJson;
    private RecipesList recipesList;
    
    [SerializeField] private GameObject recipeButtonsParent;
    [SerializeField] private GameObject recipeButtonPrefab;
    private List<GameObject> recipeButtons;

    private void Start() {
        handler = handlerGO.GetComponent<RecipesSelectionHandler>();
        recipesList = new RecipesList();
        recipesList = JsonUtility.FromJson<RecipesList>(recipesJson.text);
        // foreach (var rec in recipesList.recipesList) { Debug.Log(rec); }
        
        InitializeRecipesButtons();
    }

    private void InitializeRecipesButtons() {
        recipeButtons = new List<GameObject>();

        foreach (var recipe in recipesList.recipesList) {
            if (!recipe.enabled) continue;
            
            var buttonRecipe = Instantiate(recipeButtonPrefab, recipeButtonsParent.transform, false);
            var buttonScript = buttonRecipe.GetComponent<RecipesButtonsHandler>();
            buttonScript.SetRecipe(recipe);
            buttonScript.SetHandler(handler);
            
            recipeButtons.Add(buttonRecipe);
        }
    }
}

[Serializable] public class Recipe {
    
    public string recipeName;
    public string recipeDescription;
    
    public string item1ID;
    public string item1Name;
    public int item1QuantityReq;
    
    public string item2ID;
    public string item2Name;
    public int item2QuantityReq;
    
    public string itemResultID;
    public string itemResultName;
    public int itemResultQuantity;
    
    public bool enabled;

    public Recipe(string recipeName, string recipeDescription, string item1ID, string item1Name, int item1QuantityReq, string item2ID, string item2Name, int item2QuantityReq, string itemResultID, string itemResultName, int itemResultQuantity, bool enabled) {
        this.recipeName = recipeName;
        this.recipeDescription = recipeDescription;
        this.item1ID = item1ID;
        this.item1Name = item1Name;
        this.item1QuantityReq = item1QuantityReq;
        this.item2ID = item2ID;
        this.item2Name = item2Name;
        this.item2QuantityReq = item2QuantityReq;
        this.itemResultID = itemResultID;
        this.itemResultName = itemResultName;
        this.itemResultQuantity = itemResultQuantity;
        this.enabled = enabled;
    }
    
    public override string ToString() {
        return $"{recipeName}, {recipeDescription}, {item1ID}, {item1Name}, {item1QuantityReq}, {item2ID}, {item2Name}, {item2QuantityReq}," +
               $"{itemResultID}, {itemResultName}, {itemResultQuantity}, {enabled}";
    }
}


[Serializable] public class RecipesList { public List<Recipe> recipesList; }