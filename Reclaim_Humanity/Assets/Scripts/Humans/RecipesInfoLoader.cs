using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class RecipesInfoLoader : MonoBehaviour {
    
    [SerializeField] private TextAsset recipesJsonTextAsset;
    
    private List<Recipe> recipesList;
    private string persistentRecipePath;

    public List<Recipe> GetRecipeList() { return recipesList; }

    private void Awake() {
        CreatePaths();
    }
    
    private void Start() {
        LoadRecipes();
    }

    public void CreatePaths()
    {
        persistentRecipePath = Path.Combine(Application.persistentDataPath, "Resources/recipes.json");
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Resources")))
        {
            CreatePersistentFolders.GetInstance()
                .GeneratePersistentFolder(Path.Combine(Application.persistentDataPath, "Resources"));
        }
    }

    public void LoadRecipes()
    {
        if (!File.Exists(persistentRecipePath))
        {
            File.WriteAllText(persistentRecipePath, recipesJsonTextAsset.text);
        }
        var recipesJson = File.ReadAllText(persistentRecipePath);
        recipesList = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson);
    }
    
    public void CleanData()
    {
        var path = Path.Combine(Application.persistentDataPath, "Resources");
        if (Directory.Exists(path))
        {
            // Directory.Delete(path, true);
            CreatePaths();
            LoadRecipes();
        }
    }

    private void OnDisable() { SerializeJson(); }

    private void SerializeJson() {
        File.WriteAllText(persistentRecipePath, JsonConvert.SerializeObject(recipesList, Formatting.Indented));
    }

    public void SaveRecipes()
    {
        SerializeJson();
    }

    public Tuple<bool, string> UnlockRecipe(string recipeUnlockedID) {
        var recipe = recipesList.Find((recipe) => recipe.recipeID == recipeUnlockedID);
        var _enabled = recipe.enabled; recipe.enabled = true;
        // SerializeJson();
        
        return new Tuple<bool, string>(_enabled, recipe.recipeName);
    }
    
}
