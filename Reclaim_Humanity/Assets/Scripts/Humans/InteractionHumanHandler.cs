using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class InteractionHumanHandler : MonoBehaviour {
    
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private string humanID;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject humanDialogueEnabler;
    [SerializeField] private GameObject unlockedANDRewardsUI;
    
    private RecipesInfoLoader _recipesInfoLoader;
    
    [SerializeField] private TextAsset recipesJsonTextAsset;
    
    private Human human;
    private DialogueHandler dialogueHandler;
    private PlayerMovement playerMovement;
    private OpenInventoryScript openInventory;
    private HumansInfoLoader infoLoader;
    private VisualizeUnlocked visualizeUnlocked;
    
    private string recipesJson;
    private List<Recipe> recipesList;
    
    private bool clickedGoOnButton;
    
    private string persistentRecipePath;

    public Human Human {
        get => human;
        set => human = value;
    }

    private void Awake() {
        dialogueHandler = dialogueUI.GetComponent<DialogueHandler>();
        playerMovement = player.GetComponent<PlayerMovement>();
        openInventory = player.GetComponent<OpenInventoryScript>();
        infoLoader = GameManager.humansInfoLoader;
        visualizeUnlocked = unlockedANDRewardsUI.GetComponent<VisualizeUnlocked>();
        _recipesInfoLoader = GameManager.recipesInfoLoader;
        dialogueUI.SetActive(false);
    }

    // private void Start() {
    //     if (!File.Exists(persistentRecipePath)) { File.WriteAllText(persistentRecipePath, recipesJsonTextAsset.text); }
    //     recipesJson = File.ReadAllText(persistentRecipePath);
    //     recipesList = JsonConvert.DeserializeObject<List<Recipe>>(recipesJson);
    // }

    public void InitiateDialogue() {
        dialogueHandler.SetActiveHuman(this);
        clickedGoOnButton = false;
        human ??= infoLoader.GetHumanById(humanID);
        
        dialogueHandler.WriteNameText(human.humanName);
        dialogueUI.SetActive(true);
        openInventory.Finished = false;
        
        // playerMovement.WalkPlayerToPosition(gameObject.transform.position + new Vector3(0.0f, -2.05f, 0.0f)); TODO
        
        StartCoroutine(StartDialogue(!human.spokenTo));
    }

    public void DisableDialogue() {
        StopAllCoroutines();
        openInventory.Finished = true;
        openInventory.OpenCloseUIFunc(true);
        
        if(dialogueUI != null) { dialogueUI.SetActive(false); }
        dialogueHandler.SetActiveHuman(null);
        dialogueHandler.EmptyNameText();
    }
    
    public void ClickedGoOnButton() { clickedGoOnButton = true; }

    private IEnumerator StartDialogue(bool firstTime) {
        var dialogue = firstTime ? human.firstDialogue : human.generalDialogue;
        dialogue.ResetPhraseNum();
        
        while(true) {
            var phrase = dialogue.GetNextPhrase();
            if (phrase == null) { break; }
            
            var coroutine = StartCoroutine(dialogueHandler.WriteSlowText(phrase));
            yield return new WaitUntil(() => clickedGoOnButton);
            StopCoroutine(coroutine);
            dialogueHandler.EmptyText();
            clickedGoOnButton = false;
        }

        if (human.recipesUnlockedID != "") {
            var recipeEnabled = _recipesInfoLoader.UnlockRecipe(human.recipesUnlockedID);
            if (!recipeEnabled.Item1) { visualizeUnlocked.StartShowUnlockedMessage("recipe", recipeEnabled.Item2); }
        }
        infoLoader.ToggleSpokenTo(human.humanID);

        clickedGoOnButton = false;
        humanDialogueEnabler.SetActive(false);
    }
}