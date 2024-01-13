using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueHandler : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI dialogueUItext;
    [SerializeField] private TextMeshProUGUI nameUIText;
    [SerializeField] private float lettersDelay = 0.05f;

    [SerializeField] private AudioClip DialogueDown;
    
    private InteractionHumanHandler activeHuman;

    // private void Awake() { gameObject.SetActive(false); }

    public void WriteFastText(string phrase) { dialogueUItext.text = phrase; }

    public IEnumerator WriteSlowText(string phrase) {
        activeHuman.SetIsTyping(true);
        EmptyText();
        
        SoundFXManager.instance.PlaySoundFXClip(DialogueDown, transform,1f);

        for (var i = 0; i < phrase.Length; i ++) {
            var letter = phrase[i];
            
            if (letter == '<') {
                int j;
                for (j = i; phrase[j] != '>'; j++) { dialogueUItext.text += phrase[j]; }
                i = j;
                letter = phrase[i];
            }

            dialogueUItext.text += letter;
            yield return new WaitForSeconds(Random.Range(0, lettersDelay));
        }
        activeHuman.SetIsTyping(false);
    }
    
    public void EmptyText() { WriteFastText(""); }

    public void WriteNameText(string humanName) { nameUIText.text = humanName; }
    
    public void EmptyNameText() { nameUIText.text = ""; }

    public void SetActiveHuman(InteractionHumanHandler human) { activeHuman = human; }

    public void NextPhraseButtonClicked() { if (activeHuman != null) { activeHuman.ClickedGoOnButton(); } }
}
