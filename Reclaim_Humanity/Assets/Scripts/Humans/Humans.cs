using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Humans : MonoBehaviour {
    
    public string HumanID { get; set; }
    public string Name { get; set; }
    public string Biome { get; set; }
    public List<string> RecipesUnlockedID { get; set; }
    public bool SpokenTo { get; set; }
    public Dialogue Dialogue { get; set; }
    
    public Humans(string humanID, string humanName, string biome, List<string> recipesUnlockedID, bool spokenTo, Dialogue dialogue) {
        this.HumanID = humanID;
        this.Name = humanName;
        this.Biome = biome;
        this.RecipesUnlockedID = recipesUnlockedID;
        this.SpokenTo = spokenTo;
        this.Dialogue = dialogue;
    }
}

public class Dialogue {
    
    public string Type { get; set; }
    public List<string> Phrases { get; set; }
    
    public Dialogue(string type, List<string> phrases) {
        this.Type = type;
        this.Phrases = phrases;
    }
}
