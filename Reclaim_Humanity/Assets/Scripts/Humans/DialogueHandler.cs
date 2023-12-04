using TMPro;
using UnityEngine;

public class DialogueHandler : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI dialogueUItext;

    public void WriteFastText(string phrase) { dialogueUItext.text = phrase; }
    
    public void WriteSlowText(char letter) { dialogueUItext.text += letter; }
    
    public void EmptyText() { WriteFastText(""); }

}
