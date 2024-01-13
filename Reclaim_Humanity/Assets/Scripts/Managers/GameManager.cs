using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Party completeParty;  // all potential members of party
    public static List<CreatureBase> party;
    public static List<int> partyLevels;
    public static List<int> partyHps;
    public static List<CreatureBase> enemies;
    public static List<int> enemiesLevels;
    public static string previousSceneName;
    public static string currentSceneName;
    public static Vector3 previousPosition;
    public static string filePath;
    public static string sceneToLoad;
    public static HumansInfoLoader humansInfoLoader;
    public static RecipesInfoLoader recipesInfoLoader;
    public static List<InventoryItem> ordinaryItemsInInventory;
    public static List<InventoryItem> specialItemsInInventory;
    
    public static float energyInLab;
    public static VolumeConfiguration volumeConfig;
    
    public static SaveNumOfHumansTalkedTo humansTalkedTo;
    public static bool buddy1;
    public static bool buddy2;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
            return; 
        }
        
        filePath = Path.Combine(Application.persistentDataPath, "savegame.dat");
        LoadFreshData(); // to remove
        humansInfoLoader = GameObject.Find("HumanInfoLoader").GetComponent<HumansInfoLoader>();
        recipesInfoLoader = GameObject.Find("RecipesInfoLoader").GetComponent<RecipesInfoLoader>();
        volumeConfig = new VolumeConfiguration();
    }

    public static void LoadFreshData()
    {
        completeParty = GameObject.Find("Party").GetComponent<Party>();
        
        party = new List<CreatureBase>();
        partyLevels = new List<int>();
        for (int i = 0; i < completeParty.party.Count; i++)
        {
            if (completeParty.party[i].CreatureName == "Wollo")
            {
                party.Add(completeParty.party[i]);
                partyLevels.Add(completeParty.partyLevels[i]);
            }
            
            //
            /*party.Add(completeParty.party[i]);
            partyLevels.Add(completeParty.partyLevels[i]);*/
            //
        }
        
        partyHps = new List<int>();
        for (int i = 0; i < party.Count; i++)
        {
            partyHps.Add(1000);
        }
        
        enemies = new List<CreatureBase>();
        enemiesLevels = new List<int>();
        // ******************************
        enemies = completeParty.enemies;
        enemiesLevels = completeParty.enemiesLevels;
        // ******************************
        previousPosition = Vector3.zero;
        ordinaryItemsInInventory = new List<InventoryItem>();
        specialItemsInInventory = new List<InventoryItem>();
        
        energyInLab = 0.0f;
        
        humansTalkedTo ??= new SaveNumOfHumansTalkedTo();
        humansTalkedTo.SetAllToZero();

        buddy1 = false;
        buddy2 = false;
    }
    
    public static void NewGame()
    {
        LoadFreshData();
        humansInfoLoader.CleanData();
        recipesInfoLoader.CleanData();
        currentSceneName = "Laboratory";
        sceneToLoad = "Laboratory";
        SceneManager.LoadScene("Introduction");
        SaveGame();
    }
    
    public static void GoToScene(string SceneName)
    {
        // previousSceneName = SceneManager.GetActiveScene().name;
        currentSceneName = SceneName;
        sceneToLoad = SceneName;
        SceneManager.LoadScene("LoadingScene");
        
        
        // Added to try remove forest teleport disabling player movement... Still don't know if this works...
        try {
            var inv = GameObject.Find("Player").GetComponent<OpenInventoryScript>();
            inv.CloseTeleportUI();
        }
        catch (NullReferenceException) {}
        
        
    }

    public static void EnterCombat()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        previousPosition = GameObject.Find("MainCharacter").transform.position;
        currentSceneName = "Battle";
        sceneToLoad = "Battle";
        SceneManager.LoadScene("LoadingScene");
    }
    
    public static void ExitCombat(List<InventoryItem> itemsDropped)
    {
        for (int i = 0; i < itemsDropped.Count; i++)
        {
            if (!itemsDropped[i].IsSpecialItem)
            {
                bool flag = false;
                for (int j = 0; j < ordinaryItemsInInventory.Count; j++)
                {
                    if (itemsDropped[i].ItemID == ordinaryItemsInInventory[j].ItemID)
                    {
                        ordinaryItemsInInventory[j].ItemQuantity += itemsDropped[i].ItemQuantity;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    ordinaryItemsInInventory.Add(itemsDropped[i]);
                }
            }
            else
            {
                bool flag = false;
                for (int j = 0; j < specialItemsInInventory.Count; j++)
                {
                    if (itemsDropped[i].ItemID == specialItemsInInventory[j].ItemID)
                    {
                        specialItemsInInventory[j].ItemQuantity += itemsDropped[i].ItemQuantity;
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    specialItemsInInventory.Add(itemsDropped[i]);
                }
            }
        }
        currentSceneName = previousSceneName;
        previousSceneName = SceneManager.GetActiveScene().name;
        sceneToLoad = currentSceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    
    public static void GoToMainMenu()
    {
        sceneToLoad = "MainMenu";
        SceneManager.LoadScene("LoadingScene");
    }

    public static void RestoreHps()
    {
        for (int i = 0; i < partyHps.Count; i++)
        {
            partyHps[i] = 1000;
        }
    }
    
    public static void HealPartyMember(string name, int amount)
    {
        for (int i = 0; i < partyHps.Count; i++)
        {
            if (party[i].CreatureName == name)
            {
                Creature partyMember = new Creature(party[i], partyLevels[i], partyHps[i]);
                partyMember.HealHPs(amount);
                partyHps[i] = partyMember.HP;
            }
        }
    }
    
    public static void IncreasePartyLevel()
    {
        for (int i = 0; i < partyLevels.Count; i++)
        {
            partyLevels[i] += 1;
        }
    }

    public static void AddBuddy(string buddyName)
    {
        for (int i = 0; i < completeParty.party.Count; i++)
        {
            if (completeParty.party[i].CreatureName == buddyName)
            {
                party.Add(completeParty.party[i]);
                partyLevels.Add(partyLevels[0]);
                partyHps.Add(1000);
            }
        }
    }
    
    public static void UpdateCombatItems(List<InventoryItem> combatItems)
    {
        ordinaryItemsInInventory.RemoveAll(obj => obj.OnCombat == true);
        foreach (var item in combatItems)
        {
            ordinaryItemsInInventory.Add(item);
        }
    }
    
    public static void SaveGame()
    {
        humansInfoLoader.SaveHumans();
        recipesInfoLoader.SaveRecipes();
        GameData data = new GameData();
        List<string> partyNames = new List<string>();
        for (int i = 0; i < party.Count; i++)
        {
            partyNames.Add(party[i].name);
        }
        data.partyNames = partyNames;
        data.partyLevels = partyLevels;
        data.partyHps = partyHps;
        data.currentSceneName = currentSceneName;

        List<string> itemIds = new List<string>();
        List<int> itemQuantities = new List<int>();
        foreach (var item in ordinaryItemsInInventory)
        {
            itemIds.Add(item.ItemID);
            itemQuantities.Add(item.ItemQuantity);
        }
        foreach (var item in specialItemsInInventory)
        {
            itemIds.Add(item.ItemID);
            itemQuantities.Add(item.ItemQuantity);
        }
        data.itemIds = itemIds;
        data.itemQuantities = itemQuantities;
        
        try
        {
            previousPosition = GameObject.Find("MainCharacter").transform.position;
        }
        catch(Exception ex)
        {
            Console.WriteLine("An exception occurred: " + ex.Message);
            previousPosition = Vector3.zero;
        }
        
        data.previousPosition = new float[3];
        data.previousPosition[0] = previousPosition.x;
        data.previousPosition[1] = previousPosition.y;
        data.previousPosition[2] = previousPosition.z;
        
        data.energyInLab = energyInLab;
        data.savedHumansTalkedTo = humansTalkedTo;

        data.buddy1 = buddy1;
        data.buddy2 = buddy2;
        
        previousPosition = Vector3.zero;
        
        BinaryFormatter formatter = new BinaryFormatter();

        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fileStream, data);
                fileStream.Close();
            }

            //Debug.Log("Game data saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save game data: " + e.Message);
        }
    }

    // Load game data from a binary file
    public static void LoadGame()
    {
        humansInfoLoader.LoadHumans();
        recipesInfoLoader.LoadRecipes();
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    GameData data = (GameData)formatter.Deserialize(fileStream);
                    List<string> partyNames = data.partyNames;
                    party = new List<CreatureBase>();
                    Party myParty = GameObject.Find("Party").GetComponent<Party>();
                    for (int i = 0; i < partyNames.Count; i++)
                    {
                        foreach (var playerBase in myParty.party)
                        {
                            if (partyNames[i] == playerBase.name)
                            {
                                party.Add(playerBase);
                            }
                        }
                    }
                    partyLevels = data.partyLevels;
                    partyHps = data.partyHps;
                    currentSceneName = data.currentSceneName;
                    
                    Vector3 position;
                    position.x = data.previousPosition[0];
                    position.y = data.previousPosition[1];
                    position.z = data.previousPosition[2];
                    previousPosition = position;
                    
                    energyInLab = data.energyInLab;
                    humansTalkedTo = data.savedHumansTalkedTo;

                    buddy1 = data.buddy1;
                    buddy2 = data.buddy2;

                    List<string> itemsIds = data.itemIds;
                    List<int> itemQuantities = data.itemQuantities;
                    ItemLoader itemLoader = GameObject.Find("InventoryItemsLoader").GetComponent<ItemLoader>();
                    List<ItemsSO> ordinaryItemsSos = new List<ItemsSO>();
                    List<ItemsSO> specialItemsSos = new List<ItemsSO>();
                    // print(itemLoader.ItemsSos.Count);
                    for (int i = 0; i < itemsIds.Count; i++)
                    {
                        foreach (var itemSo in itemLoader.ItemsSos)
                        {
                            print("hello");
                            if (itemsIds[i] == itemSo.ItemID)
                            {
                                if (itemSo.IsItemSpecial)
                                {
                                    specialItemsSos.Add(itemSo);
                                }
                                else
                                {
                                    ordinaryItemsSos.Add(itemSo);
                                }
                            }
                        }
                    }

                    ordinaryItemsInInventory = new List<InventoryItem>();
                    specialItemsInInventory = new List<InventoryItem>();
                    for (int i = 0; i < ordinaryItemsSos.Count; i++)
                    {
                        ordinaryItemsInInventory.Add(ordinaryItemsSos[i].ToInventoryItem(itemQuantities[i]));
                    }

                    for (int i = 0; i < specialItemsSos.Count; i++)
                    {
                        specialItemsInInventory.Add(specialItemsSos[i].
                            ToInventoryItem(itemQuantities[i+ordinaryItemsSos.Count]));
                    }
                    GoToScene(currentSceneName);
                    Debug.Log("Game data loaded successfully.");
                    fileStream.Close();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load game data: " + e.Message);
            }
        }
        else
        {
            Debug.Log("No saved game data found.");
        }
    }
}

[Serializable]
public class GameData
{
    public List<string> partyNames;
    public List<int> partyLevels;
    public List<int> partyHps;
    public string currentSceneName;
    public float[] previousPosition;
    public List<string> itemIds;
    public List<int> itemQuantities;
    
    public float energyInLab;
    public SaveNumOfHumansTalkedTo savedHumansTalkedTo;
    public bool buddy1;
    public bool buddy2;
}

public class VolumeConfiguration {
    private float masterVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float soundsVolume = 1.0f;
    
    public float GetMasterVolume() { return masterVolume; }
    public float GetMusicVolume() { return musicVolume; }
    public float GetSoundsVolume() { return soundsVolume; }
    
    public void SetMasterVolume(float _masterVolume) { masterVolume = _masterVolume; }
    public void SetMusicVolume(float _musicVolume) { musicVolume = _musicVolume; }
    public void SetSoundsVolume(float _soundsVolume) { soundsVolume = _soundsVolume; }

    public void SetAllVolumes(List<float> volumes) { masterVolume = volumes[0]; musicVolume = volumes[1]; soundsVolume = volumes[2]; }
    public List<float> GetAllVolumes() { return new List<float>() {masterVolume, musicVolume, soundsVolume}; }
}

[Serializable]
public class SaveNumOfHumansTalkedTo {
    
    private int HumansInLaboratory { get; set; }
    private int HumansInForest { get; set; }
    private int HumansInCity { get; set; }
    private int HumansInWastelands { get; set; }
    
    public void SetAllToZero () {
        HumansInLaboratory = 0;
        HumansInForest = 0;
        HumansInCity = 0;
        HumansInWastelands = 0;
    }
    
    public void SetThemAll(int lab, int forest, int city, int wastelands) {
        HumansInLaboratory = lab;
        HumansInForest = forest;
        HumansInCity = city;
        HumansInWastelands = wastelands;
    }
    
    public int GetHumansByBiome(string biome) {
        return biome switch {
            "Laboratory" => HumansInLaboratory,
            "OvergrownForest" => HumansInForest,
            "RuinedCity" => HumansInCity,
            "Wastelands" => HumansInWastelands,
            _ => 0
        };
    }
    
    public void SetHumansByBiome(string biome, int quantity) {
        switch (biome) {
            case "Laboratory": { HumansInLaboratory = quantity; break; }
            case "OvergrownForest" : { HumansInForest = quantity; break;}
            case "RuinedCity" : { HumansInCity = quantity; break; }
            case "Wastelands" : { HumansInWastelands = quantity; break; }
            default : { SetAllToZero(); break; }
        }
    }
}
