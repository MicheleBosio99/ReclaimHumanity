using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<BattleUnit> playerUnits;
    [SerializeField] private List<BattleHud> playerHuds;
    [SerializeField] private List<BattleUnit> enemyUnits;
    [SerializeField] private List<BattleHud> enemyHuds;

    [SerializeField] private List<CreatureBase> playerBases;
    [SerializeField] private List<int> playerLevels;
    [SerializeField] private List<CreatureBase> enemyBases;
    [SerializeField] private List<int> enemyLevels;
    
    [SerializeField] private BattleDialogBox dialogBox;

    private void Start()
    {
        SetupBattle();
    }

    public void SetupBattle()
    {
        for (int i = 0; i < playerBases.Count; i++)
        {
            playerUnits[i].SetUp(playerBases[i], playerLevels[i]);
            playerHuds[i].SetData(playerUnits[i].Creature);
        }
        
        for (int i = 0; i < enemyBases.Count; i++)
        {
            enemyUnits[i].SetUp(enemyBases[i], enemyLevels[i]);
            enemyHuds[i].SetData(enemyUnits[i].Creature);
        }
        
        dialogBox.SetDialog("Oh no, some enemies :(");
    }
}
