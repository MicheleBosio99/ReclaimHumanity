using UnityEngine;

public class HumanDialogueEnabler : MonoBehaviour {
    
    [SerializeField] private GameObject human;
    private InteractionHumanHandler interactionHumanHandler;
    
    private bool firstTime = true;

    private void Awake() {
        interactionHumanHandler = human.GetComponent<InteractionHumanHandler>();
    }

    private void OnEnable() {
        if (firstTime) { return; }
        interactionHumanHandler.InitiateDialogue();
    }

    private void OnDisable() {
        if (firstTime) { firstTime = false; return; }
        interactionHumanHandler.DisableDialogue();
    }
}
