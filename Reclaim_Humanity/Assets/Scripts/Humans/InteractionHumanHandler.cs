using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHumanHandler : MonoBehaviour {
    
    [SerializeField] private GameObject dialogueUI;
    
    private Humans human;
    private DialogueHandler dialogueHandler;
    
    private bool isTyping;
    private bool hasFinished;
    private bool clickedGoOnButton;

    private void Start() {
        human = gameObject.GetComponent<Humans>();
        dialogueHandler = dialogueUI.GetComponent<DialogueHandler>();
        isTyping = false;
        hasFinished = false;
        clickedGoOnButton = false;
    }
    
    public void NextPhraseButtonClicked() { clickedGoOnButton = true; }

    public void StartHumanInteraction(InputAction.CallbackContext context) { clickedGoOnButton = true; }

    private void Update() {
        if (isTyping || hasFinished || !clickedGoOnButton) return;
        
        clickedGoOnButton = false;
            
        var phrase = human.Dialogue.GetNextPhrase();
        if (phrase == null) { hasFinished = true; return; }

        StartCoroutine(DisplayDialogue(phrase));
    }

    private IEnumerator DisplayDialogue(string phrase) {
        isTyping = true;

        foreach (var letter in phrase.ToCharArray()) {
            dialogueHandler.WriteSlowText(letter);
            yield return new WaitForSeconds(0.2f);
        }
        
        isTyping = false;
    }
}
