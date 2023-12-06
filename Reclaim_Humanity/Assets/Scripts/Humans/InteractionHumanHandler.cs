using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHumanHandler : MonoBehaviour {
    
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private string humanID;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject humansInfoLoader;
    [SerializeField] private GameObject humanDialogueEnabler;
    
    private Human human;
    private DialogueHandler dialogueHandler;
    private PlayerMovement playerMovement;
    private OpenInventoryScript openInventory;
    private HumansInfoLoader infoLoader;
    
    private bool clickedGoOnButton;

    private void Awake() {
        dialogueHandler = dialogueUI.GetComponent<DialogueHandler>();
        playerMovement = player.GetComponent<PlayerMovement>();
        openInventory = player.GetComponent<OpenInventoryScript>();
        infoLoader = humansInfoLoader.GetComponent<HumansInfoLoader>();
        dialogueUI.SetActive(false);
    }

    private void Start() {
        dialogueHandler.SetActiveHuman(this);
        clickedGoOnButton = false;
    }

    public void InitiateDialogue() {
        if (human == null) { human = infoLoader.GetHumanById(humanID); }
        human.dialogue.ResetPhraseNum();
        dialogueUI.SetActive(true);
        openInventory.Finished = false;
        
        playerMovement.WalkPlayerToPosition(gameObject.transform.position + new Vector3(0.0f, -2.05f, 0.0f));
        // StartCoroutine(WaitForXSeconds(2.0f));
        
        StartCoroutine(StartDialogue());
    }
    
    private IEnumerator WaitForXSeconds(float seconds) { yield return new WaitForSeconds(seconds); }

    public void DisableDialogue() {
        StopAllCoroutines();
        openInventory.Finished = true;
        dialogueUI.SetActive(false);
        dialogueHandler.SetActiveHuman(null);
    }
    
    public void ClickedGoOnButton() { clickedGoOnButton = true; }

    private IEnumerator StartDialogue() {
        human.dialogue.ResetPhraseNum();
        
        while(true) {
            var phrase = human.dialogue.GetNextPhrase();
            if (phrase == null) { break; }
            
            var coroutine = StartCoroutine(dialogueHandler.WriteSlowText(phrase));
            yield return new WaitUntil(() => clickedGoOnButton);
            StopCoroutine(coroutine);
            dialogueHandler.EmptyText();
            clickedGoOnButton = false;
        }
        // yield return new WaitForSeconds(1.0f);
        clickedGoOnButton = false;
        humanDialogueEnabler.SetActive(false);
    }
}