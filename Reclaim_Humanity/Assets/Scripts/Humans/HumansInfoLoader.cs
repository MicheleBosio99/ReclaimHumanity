using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class HumansInfoLoader : MonoBehaviour {
    
    [SerializeField] private TextAsset humansJsonTextAssets;
    [SerializeField] private string biome;
    private List<Human> humans;
    
    private string humansJson;
    private string persistentHumanPath;
    
    private void Awake() {
        persistentHumanPath = Path.Combine(Application.persistentDataPath, $"Resources/Humans/humans_{biome}.json");
        CreatePersistentFolders.GetInstance().GeneratePersistentFolder(Path.Combine(Application.persistentDataPath, "Resources"));
        CreatePersistentFolders.GetInstance().GeneratePersistentFolder(Path.Combine(Application.persistentDataPath, "Resources/Humans"));
    }

    private void Start() {
        if (!File.Exists(persistentHumanPath)) { File.WriteAllText(persistentHumanPath, humansJsonTextAssets.text); }
        humansJson = File.ReadAllText(persistentHumanPath);
        
        humans = new List<Human>();
        humans = JsonConvert.DeserializeObject<List<Human>>(humansJson);
    }

    public Human GetHumanById(string humanID) { return humans.Find(human => human.humanID == humanID); }

    public void ToggleSpokenTo(string humanID) {
        var human = humans.Find(hum => hum.humanID == humanID);
        human.spokenTo = true;
        File.WriteAllText(persistentHumanPath, JsonConvert.SerializeObject(humans, Formatting.Indented));
    }
}

[Serializable] public class Human {
    
    public string humanID;
    public string humanName;
    public string biome;
    public string recipesUnlockedID;
    public bool spokenTo;
    public Dialogue firstDialogue;
    public Dialogue generalDialogue;

    public override string ToString() { return $"ID: {humanID}, name: {humanName}, firstDialogue: [{firstDialogue}]"; }
}

[Serializable] public class Dialogue {
    
    public List<string> phrases;
    private int phraseNum;

    public string GetNextPhrase() {
        if(phraseNum.Equals(phrases.Count)) { return null; }
        phraseNum ++;
        return phrases[phraseNum - 1];
    }

    public void ResetPhraseNum() { phraseNum = 0; }

    public override string ToString() { return $"Phrases: { phrases.Aggregate((current, next) => current + ", " + next) }"; }
}