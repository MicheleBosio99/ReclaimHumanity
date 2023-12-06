using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueHandler : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI dialogueUItext;
    [SerializeField] private float lettersDelay = 0.1f;
    
    private InteractionHumanHandler activeHuman;

    private void WriteFastText(string phrase) { dialogueUItext.text = phrase; }

    public IEnumerator WriteSlowText(string phrase) {
        EmptyText();

        foreach (var letter in phrase) {
            dialogueUItext.text += letter;
            yield return new WaitForSeconds(Random.Range(0, lettersDelay));
        }
    }
    
    public void EmptyText() { WriteFastText(""); }

    public void SetActiveHuman(InteractionHumanHandler human) { activeHuman = human; }

    public void NextPhraseButtonClicked() { if (activeHuman != null) { activeHuman.ClickedGoOnButton(); } }

}
