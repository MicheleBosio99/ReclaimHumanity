using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        /*enemies = new List<CreatureBase>();
        enemiesLevels = new List<int>();*/
        enemies = myParty.enemies;
        enemiesLevels = myParty.enemiesLevels;
        previousPosition = Vector3.zero;
    }

    public static void NewGame()
    {
        LoadFreshData();
        SaveGame();
        GoToScene("Laboratory");
    }
    
    public static void GoToScene(string SceneName)
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        currentSceneName = SceneName;
        SceneManager.LoadScene(currentSceneName);
    }

    public static void EnterCombat()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        previousPosition = GameObject.Find("MainCharacter").transform.position;
        currentSceneName = "Battle";
        SceneManager.LoadScene(currentSceneName);
    }
    
    public static void ExitCombat()
    {
        currentSceneName = previousSceneName;
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    
    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
        previousPosition = GameObject.Find("MainCharacter").transform.position;
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

            Debug.Log("Game data saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save game data: " + e.Message);
        }
    }

    // Load game data from a binary file
    public static void LoadGame()
    {
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

[System.Serializable]
public class GameData
{
    public List<string> partyNames;
    public List<int> partyLevels;
    public List<int> partyHps;
    public string currentSceneName;
    public float[] previousPosition;
}
