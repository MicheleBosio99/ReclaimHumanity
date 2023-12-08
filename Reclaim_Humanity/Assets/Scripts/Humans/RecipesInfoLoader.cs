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

    private void Awake() {
        persistentRecipePath = Path.Combine(Application.persistentDataPath, "Resources/recipes.json");
        CreatePersistentFolders.GetInstance().GeneratePersistentFolder(Path.Combine(Application.persistentDataPath, "Resources"));
    }

    private void Start() {
        if (!File.Exists(persistentRecipePath)) { File.WriteAllText(persistentRecipePath, recipesJsonTextAsset.text); }
        var recipesJson = File.ReadAllText(persistentRecipePath);
        recipesList = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson);
    }

    private void OnDisable() { SerializeJson(); }

    private void SerializeJson() {
        File.WriteAllText(persistentRecipePath, JsonConvert.SerializeObject(recipesList, Formatting.Indented));
    }

    public Tuple<bool, string> UnlockRecipe(string recipeUnlockedID) {
        var recipe = recipesList.Find((recipe) => recipe.recipeID == recipeUnlockedID);
        var _enabled = recipe.enabled; recipe.enabled = true;
        SerializeJson();
        
        return new Tuple<bool, string>(_enabled, recipe.recipeName);
    }
    
}
