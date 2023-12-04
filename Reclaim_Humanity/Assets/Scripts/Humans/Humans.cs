using System.Collections.Generic;
using UnityEngine;

public class Humans : MonoBehaviour {
    
    public string HumanID { get; set; }
    public string Name { get; set; }
    public string Biome { get; set; }
    public List<string> RecipesUnlockedID { get; set; }
    public bool SpokenTo { get; set; }
    public Dialogue Dialogue { get; set; }
    
    public Humans(string humanID, string humanName, string biome, List<string> recipesUnlockedID, bool spokenTo, Dialogue dialogue) {
        HumanID = humanID;
        Name = humanName;
        Biome = biome;
        RecipesUnlockedID = recipesUnlockedID;
        SpokenTo = spokenTo;
        Dialogue = dialogue;
    }
}

public class Dialogue {
    public string Type { get; set; }
    public List<string> Phrases { get; set; }
    
    public Dialogue(string type, List<string> phrases) {
        Type = type;
        Phrases = phrases;
    }
    
    private int phraseNum = 0;

    public string GetNextPhrase() {
        if (phraseNum.Equals(Phrases.Count)) { return null; }

        var phrase = Phrases[phraseNum];
        phraseNum ++;
        return phrase;
    }

    public void ResetPhraseNum() { phraseNum = 0; }
}
