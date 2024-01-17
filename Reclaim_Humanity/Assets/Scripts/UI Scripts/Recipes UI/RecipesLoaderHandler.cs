using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipesLoaderHandler : MonoBehaviour {
    
    [SerializeField] private GameObject handlerGO;
    private RecipesSelectionHandler handler;
    [SerializeField] private TextAsset recipesJsonTextAsset;
    [SerializeField] private GameObject noRecipesEnabledPanel;
    
    private string recipesJson;
    private List<Recipe> recipesList;
    
    [SerializeField] private GameObject recipeButtonsParent;
    [SerializeField] private GameObject recipeButtonPrefab;
    private List<GameObject> recipeButtons;
    
    private string persistentRecipePath;

    // private void Awake() {
    //     persistentRecipePath = Path.Combine(Application.persistentDataPath, "Resources/recipes.json");
    //     if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Resources"))) {
    //         CreatePersistentFolders.GetInstance()
    //             .GeneratePersistentFolder(Path.Combine(Application.persistentDataPath, "Resources"));
    //     }
    // }

    private void Start() {
        // if (!File.Exists(persistentRecipePath)) { File.WriteAllText(persistentRecipePath, recipesJsonTextAsset.text); }
        // recipesJson = File.ReadAllText(persistentRecipePath);
        
        // recipesList = new List<Recipe>();
        // recipesList = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson);
        
        recipesList = GameManager.recipesInfoLoader.GetRecipeList();
        noRecipesEnabledPanel.SetActive(false);
        
        handler = handlerGO.GetComponent<RecipesSelectionHandler>();
        InitializeRecipesButtons();
    }

    private void InitializeRecipesButtons() {
        recipeButtons = new List<GameObject>();
        
        foreach (var recipe in recipesList) {
            if (!recipe.enabled) continue;
            
            var buttonRecipe = Instantiate(recipeButtonPrefab, recipeButtonsParent.transform, false);
            var buttonScript = buttonRecipe.GetComponent<RecipesButtonsHandler>();
            buttonScript.SetRecipe(recipe);
            buttonScript.SetHandler(handler);
            
            recipeButtons.Add(buttonRecipe);
        }
        
        if (recipeButtons.Count == 0) { noRecipesEnabledPanel.SetActive(true); }
    }
}

[Serializable] public class Recipe {
    
    public string recipeID;
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
    
    public override string ToString() {
        return $"{recipeName}, {recipeDescription}, {item1ID}, {item1Name}, {item1QuantityReq}, {item2ID}, {item2Name}, {item2QuantityReq}," +
               $"{itemResultID}, {itemResultName}, {itemResultQuantity}, {enabled}";
    }
}