using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
    
    public static List<InventoryItem> itemsDropped;
    
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
    }

    public static void LoadFreshData()
    {
        Party myParty = GameObject.Find("Party").GetComponent<Party>();
        party = myParty.party;
        partyLevels = myParty.partyLevels;
        partyHps = new List<int>();
        for (int i = 0; i < party.Count; i++)
        {
            partyHps.Add(1000);
        }
        enemies = new List<CreatureBase>();
        enemiesLevels = new List<int>();
        /*enemies = myParty.enemies;
        enemiesLevels = myParty.enemiesLevels;*/
        previousPosition = Vector3.zero;
        ordinaryItemsInInventory = new List<InventoryItem>();
        specialItemsInInventory = new List<InventoryItem>();
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
    }

    public static void EnterCombat()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        previousPosition = GameObject.Find("MainCharacter").transform.position;
        currentSceneName = "Battle";
        sceneToLoad = "Battle";
        SceneManager.LoadScene("LoadingScene");
    }
    
    public static void ExitCombat(List<InventoryItem> _itemsDropped)
    {
        itemsDropped = _itemsDropped;
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
                    // print(ordinaryItemsInInventory.Count);
                    GoToScene(currentSceneName);
                    // print(ordinaryItemsInInventory.Count);
                    // Debug.Log("Game data loaded successfully.");
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

[System.Serializable]
public class GameData
{
    public List<string> partyNames;
    public List<int> partyLevels;
    public List<int> partyHps;
    public string currentSceneName;
    public float[] previousPosition;
    public List<string> itemIds;
    public List<int> itemQuantities;
}
