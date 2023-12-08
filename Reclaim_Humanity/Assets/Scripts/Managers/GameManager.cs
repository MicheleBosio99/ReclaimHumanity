using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static List<CreatureBase> party;
    public static List<int> partyLevels;
    public static List<CreatureBase> enemies;
    public static List<int> enemiesLevels;
    public static string previousSceneName;
    public static string currentSceneName;
    public static Vector3 previousPosition;
    public static bool positionSet = false;
    
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
        Party myParty = GameObject.Find("Party").GetComponent<Party>();
        party = myParty.party;
        partyLevels = myParty.partyLevels;
        enemies = myParty.enemies;
        enemiesLevels = myParty.enemiesLevels;
        previousPosition = Vector3.zero;
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
    
}
