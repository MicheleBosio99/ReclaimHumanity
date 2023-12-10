using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public static Party instance;
    [SerializeField] public List<CreatureBase> party;
    [SerializeField] public List<int> partyLevels;
    [SerializeField] public List<CreatureBase> enemies;
    [SerializeField] public List<int> enemiesLevels;
    
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
    }
}
