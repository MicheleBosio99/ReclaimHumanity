using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHumanHandler : MonoBehaviour {
    
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private string humanID;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject humanDialogueEnabler;
    [SerializeField] private GameObject unlockedANDRewardsUI;
    
    private RecipesInfoLoader _recipesInfoLoader;
    
    [SerializeField] private TextAsset recipesJsonTextAsset;
    
    [SerializeField] private GameObject numOfHumansLeftGO;
    private NumOfHumansLeft numOfHumansLeft;
    
    [SerializeField] private bool isBuddy;
    
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
    
    private bool isTyping;

    public bool GetIsTyping() { return isTyping; }
    public void SetIsTyping(bool _isTyping) { isTyping = _isTyping; }
    public string HumanID { get => humanID; set => humanID = value; }
    

    public Human Human {
        get => human;
        set => human = value;
    }

    private void Awake() {
        isTyping = false;
        dialogueHandler = dialogueUI.GetComponent<DialogueHandler>();
        playerMovement = player.GetComponent<PlayerMovement>();
        openInventory = player.GetComponent<OpenInventoryScript>();
        infoLoader = GameManager.humansInfoLoader;
        visualizeUnlocked = unlockedANDRewardsUI.GetComponent<VisualizeUnlocked>();
        _recipesInfoLoader = GameManager.recipesInfoLoader;
        dialogueUI.SetActive(false);
        
        numOfHumansLeft = numOfHumansLeftGO.GetComponent<NumOfHumansLeft>();
        
        if(humanID == "AAA_Dr-IDK_Lab" && GameManager.hasAll3SpecialItems.hasAlreadyTalkedToIDK) { humanID = "AAA_Dr-IDK_Lab-3SpecialItems"; }
        if(humanID == "AAA_Dr-IDK_Lab-3SpecialItems" && GameManager.gameEnded) { humanID = "AAA_Dr-IDK_Lab-3EndGame"; }
    }

    public void ChangeHumanID(string newHumanID) {
        humanID = newHumanID;
    }

    public void InitiateDialogue() {
        dialogueHandler.SetActiveHuman(this);
        clickedGoOnButton = false;
        human ??= infoLoader.GetHumanById(humanID);
        
        dialogueHandler.WriteNameText(human.humanName);
        dialogueUI.SetActive(true);
        openInventory.Finished = false;
        
        StartCoroutine(StartDialogue(!human.spokenTo));
    }

    public void DisableDialogue() {
        StopAllCoroutines();
        openInventory.Finished = true;
        openInventory.OpenCloseUIFunc(true);
        
        if(dialogueUI != null) { dialogueUI.SetActive(false); }
        dialogueHandler.SetActiveHuman(null);
        dialogueHandler.EmptyNameText();

        if (humanID == "AAA_Buddy1")
        {
            GameManager.AddBuddy("OneWheelBoy");
            GameManager.buddy1 = true;
            Destroy(gameObject);
        }
        else if (humanID == "AAA_Buddy2")
        {
            GameManager.AddBuddy("FireFranco");
            GameManager.buddy2 = true;
            Destroy(gameObject);
        }
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
            
            if (isTyping) {
                StopCoroutine(coroutine);
                clickedGoOnButton = false;
                dialogueHandler.WriteFastText(phrase);
                yield return new WaitUntil(() => clickedGoOnButton);
            }
            dialogueHandler.EmptyText();
            clickedGoOnButton = false;
        }

        if (human.recipesUnlockedID != "") {
            var recipeEnabled = _recipesInfoLoader.UnlockRecipe(human.recipesUnlockedID);
            if (!recipeEnabled.Item1) {
                visualizeUnlocked.StartShowUnlockedRecipeMessage($"Congratulations! You unlocked a new recipe: <u>{recipeEnabled.Item2}</u>");
            }
        }
        if(!human.spokenTo && !isBuddy) { numOfHumansLeft.AddHumanTalkedTo(); }
        
        infoLoader.ToggleSpokenTo(human.humanID);

        clickedGoOnButton = false;
        humanDialogueEnabler.SetActive(false);
    }
}