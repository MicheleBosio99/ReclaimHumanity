using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHumans : MonoBehaviour {
    
    [SerializeField] private TextAsset humansJSON;
    private HumansList humansList;

    public HumansList HumansList {
        get => humansList;
        set => humansList = value;
    }
    
    private void Start() {
        humansList = new HumansList();
        humansList = JsonUtility.FromJson<HumansList>(humansJSON.text);
    }
}

public class HumansList {
    
    public List<Humans> humans;
    public HumansList() { }
}

public class Humans {
    
    public string humanID;
    public string name;
    public string biome;
    public List<string> recipesUnlockedID;
    public bool spokenTo;
    public Dialogue dialogue;

    public Humans(string humanID, string name, string biome, List<string> recipesUnlockedID, bool spokenTo, Dialogue dialogue) {
        this.humanID = humanID;
        this.name = name;
        this.biome = biome;
        this.recipesUnlockedID = recipesUnlockedID;
        this.spokenTo = spokenTo;
        this.dialogue = dialogue;
    }

    public string HumanID {
        get => humanID;
        set => humanID = value;
    }

    public string Name {
        get => name;
        set => name = value;
    }

    public string Biome {
        get => biome;
        set => biome = value;
    }

    public List<string> RecipesUnlockedID {
        get => recipesUnlockedID;
        set => recipesUnlockedID = value;
    }

    public bool SpokenTo {
        get => spokenTo;
        set => spokenTo = value;
    }

    public Dialogue Dialogue {
        get => dialogue;
        set => dialogue = value;
    }
}

public class Dialogue {
    
    public string type;
    public List<string> phrases;

    public Dialogue(string type, List<string> phrases) {
        this.type = type;
        this.phrases = phrases;
    }

    public string Type {
        get => type;
        set => type = value;
    }

    public List<string> Phrases {
        get => phrases;
        set => phrases = value;
    }
}
