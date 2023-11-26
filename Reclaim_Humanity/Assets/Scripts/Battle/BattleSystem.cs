using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum BattleState { Start, PlayerAction, PlayerMove, SelectTarget, EnemyMove, Busy }

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
    [SerializeField] private GameObject handL;
    [SerializeField] private GameObject handR;

    private BattleState state;
    private int currentAction;
    private int currentMove;
    private int currentCreature;
    private int currentTarget;

    private List<Move> selectedMoves;
    private List<Enemy> selectedTargets;

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
        yield return new WaitForSeconds(0.5f);

        currentCreature = 0;
        dialogBox.SetMoveNames(playerUnits[currentCreature].Creature.Moves);
        handL.SetActive(true);
        handL.transform.position = playerUnits[currentCreature].transform.position + new Vector3(1,0,0);
        StartCoroutine(PlayerAction());
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnits[currentCreature].Creature.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnits[currentCreature].Creature.Base.Name} used  {move.Base.Name} " +
                                          $"against {enemyUnits[enemyBases.Count - 1 - currentTarget].Creature.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnits[enemyBases.Count - 1 - currentTarget].Creature
            .TakeDamage(move, playerUnits[currentCreature].Creature);
        yield return enemyHuds[enemyBases.Count - 1 - currentTarget].UpdateHP();
        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnits[enemyBases.Count - 1 - currentTarget].Creature.Base.Name}" +
                                              $" fainted");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        var move = enemyUnits[enemyBases.Count - 1 - currentTarget].Creature.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnits[enemyBases.Count - 1 - currentTarget].Creature.Base.Name} used  " +
                                          $"{move.Base.Name} against {playerUnits[currentCreature].Creature.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnits[currentCreature].Creature
            .TakeDamage(move, enemyUnits[enemyBases.Count - 1 - currentTarget].Creature);
        yield return playerHuds[currentCreature].UpdateHP();
        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnits[currentCreature].Creature.Base.Name} fainted");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            if (currentCreature == playerBases.Count - 1)
            {
                currentCreature = 0;
            }
            else
            {
                currentCreature++;
            }
            currentMove = 0;
            dialogBox.SetMoveNames(playerUnits[currentCreature].Creature.Moves);
            handR.SetActive(false);
            handL.transform.position = playerUnits[currentCreature].transform.position + new Vector3(1,0,0);
            StartCoroutine(PlayerAction());
        }
    }

    IEnumerator PlayerAction()
    {
        state = BattleState.Busy;
        string text = $"It's {playerUnits[currentCreature].Creature.Base.Name}'s turn, choose an action!";
        yield return dialogBox.TypeDialog(text);
        yield return new WaitForSeconds(0.5f);
        dialogBox.EnableActionSelector(true);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableMoveSelector(false);
        state = BattleState.PlayerAction;
    }
    
    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }
    
    IEnumerator SelectTarget()
    {
        state = BattleState.Busy;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableMoveSelector(false);
        string text = "Choose the enemy to attack";
        yield return dialogBox.TypeDialog(text);
        yield return new WaitForSeconds(0.5f);
        handR.SetActive(true);
        state = BattleState.SelectTarget;
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
        else if (state == BattleState.SelectTarget)
        {
            HandleTargetSelection();
        }
        else if (state == BattleState.Busy)
        {
            
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
            StartCoroutine(SelectTarget());
        }
    }

    void HandleTargetSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentTarget < enemyBases.Count - 1)
            {
                ++currentTarget;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentTarget > 0)
            {
                --currentTarget;
            }
        }
        handR.transform.position = enemyUnits[enemyBases.Count - 1 - currentTarget].transform.position + new Vector3(-1.2f,0,0);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(PerformPlayerMove());
        }
    }
}
