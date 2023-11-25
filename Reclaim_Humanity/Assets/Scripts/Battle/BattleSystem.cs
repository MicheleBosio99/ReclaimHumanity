using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

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

    private BattleState state;
    private int currentAction;
    private int currentMove;
    private int currentCreature;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
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
        
        yield return dialogBox.TypeDialog("Oh no, some enemies :(");
        yield return new WaitForSeconds(1f);

        currentCreature = 0;
        dialogBox.SetMoveNames(playerUnits[currentCreature].Creature.Moves);
        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        string text = $"It's {playerUnits[currentCreature].Creature.Base.Name}'s turn, choose an action!";
        StartCoroutine(dialogBox.TypeDialog(text));
        dialogBox.EnableActionSelector(true);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableMoveSelector(false);
    }
    
    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
            {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
        }
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnits[currentCreature].Creature.Moves.Count - 1)
            {
                ++currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
            {
                --currentMove;
            }
        }   
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnits[currentCreature].Creature.Moves.Count - 2)
            {
                currentMove += 2;
            }
        }   
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
            {
                currentMove -= 2;
            }
        }  
        dialogBox.UpdateMoveSelection(currentMove, playerUnits[currentCreature].Creature.Moves[currentMove]);
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // choose enemy to attack
            if (currentCreature == playerBases.Count - 1)
            {
                // attack event
                Debug.Log("Attack! Pew Pew!");
                currentCreature = 0;
                PlayerAction();
            }
            else
            {
                currentCreature++;
                dialogBox.SetMoveNames(playerUnits[currentCreature].Creature.Moves);
                PlayerAction();
            }
        }
    }
}
