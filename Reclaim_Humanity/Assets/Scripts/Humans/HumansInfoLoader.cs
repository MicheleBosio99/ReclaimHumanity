using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class HumansInfoLoader : MonoBehaviour {
    
    [SerializeField] private TextAsset humansJson;
    private List<Human> humans;
    
    private void Awake() {
        humans = new List<Human>();
        humans = JsonConvert.DeserializeObject<List<Human>>(humansJson.text);
    }
    
    public Human GetHumanById(string humanID) { return humans.Find(human => human.humanID == humanID); }
}

[Serializable] public class Human : MonoBehaviour {
    
    public string humanID;
    public string humanName;
    public string biome;
    public List<string> recipesUnlockedID;
    public bool spokenTo;
    public Dialogue dialogue;

    public override string ToString() { return $"ID: {humanID}, name: {humanName}, dialogue: [{dialogue.ToString()}]"; }
}

[Serializable] public class Dialogue {
    
    public string type;
    public List<string> phrases;
    
    private int phraseNum;

    public string GetNextPhrase() {
        // if (type == "random") { phraseNum = UnityEngine.Random.Range(0, phraseNum); }
        if(phraseNum.Equals(phrases.Count)) { return null; }
        phraseNum ++;
        return phrases[phraseNum - 1];
    }

    public void ResetPhraseNum() { phraseNum = 0; }

    public override string ToString() { return $"Phrases: { phrases.Aggregate((current, next) => current + ", " + next) }"; }
}