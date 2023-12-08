using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

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
    
    private List<int> order;

    private List<Move> selectedMoves;
    private List<Enemy> selectedTargets;

    private void Start()
    {
        playerBases = GameManager.party;
        playerLevels = GameManager.partyLevels;
        enemyBases = GameManager.enemies;
        enemyLevels = GameManager.enemiesLevels;
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
        SetOrder();
        StartTurn();
    }

    private void SetOrder()
    {
        List<int> speeds = new List<int>();
        order = new List<int>();
        for (int i = 0; i < playerBases.Count; i++)
        {
            speeds.Add(playerBases[i].Speed + UnityEngine.Random.Range(0,10));
        }
        for (int i = 0; i < enemyBases.Count; i++)
        {
            speeds.Add(enemyBases[i].Speed + UnityEngine.Random.Range(0,10));
        }
        
        for (int i = 0; i < playerBases.Count + enemyBases.Count; i++)
        {
            int maxIndex = speeds.IndexOf(speeds.Max());
            order.Add(maxIndex);
            speeds[maxIndex] = 0;
        }
    }

    private void StartTurn()
    {
        if (order.Count == 0)
        {
            SetOrder();
        }
        int current = order[0];
        order.RemoveAt(0);
        if (current < playerBases.Count)
        {
            currentCreature = current;
            dialogBox.SetMoveNames(playerUnits[currentCreature].Creature.Moves);
            currentMove = 0;
            currentTarget = 0;
            handL.transform.position = playerUnits[currentCreature].transform.position + new Vector3(1,0,0);
            handL.SetActive(true);
            handR.SetActive(false);
            StartCoroutine(PlayerAction());
        }
        else
        {
            currentCreature = current - playerBases.Count;
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnits[currentCreature].Creature.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnits[currentCreature].Creature.Base.Name} used  {move.Base.Name} " +
                                          $"against {enemyUnits[currentTarget].Creature.Base.Name}");
        yield return new WaitForSeconds(1f);

        var damageDetails = enemyUnits[currentTarget].Creature.TakeDamage(move, playerUnits[currentCreature].Creature);
        yield return enemyHuds[currentTarget].UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnits[currentTarget].Creature.Base.Name}" +
                                              $" fainted");
            yield return new WaitForSeconds(1f);
            
            enemyUnits[currentTarget].gameObject.SetActive(false);
            enemyHuds[currentTarget].gameObject.SetActive(false);
            if (currentTarget < enemyBases.Count - 1)
            {
                BattleUnit item = enemyUnits[currentTarget];
                enemyUnits.RemoveAt(currentTarget);
                enemyUnits.Add(item);
                BattleHud item2 = enemyHuds[currentTarget];
                enemyHuds.RemoveAt(currentTarget);
                enemyHuds.Add(item2);
            } 
            enemyBases.RemoveAt(currentTarget);
            
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i] == currentTarget + playerBases.Count)
                {
                    order.RemoveAt(i);
                }
            }
            for (int i = 0; i < order.Count; i++) {
                if (order[i] > playerBases.Count + currentTarget)
                {
                    order[i] -= 1;
                }
            }

            if (enemyBases.Count == 0)
            {
                StartCoroutine(WinFight());
            }
            else
            {
                StartTurn();
            }
        }
        else
        {
            StartTurn();
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        currentTarget = UnityEngine.Random.Range(0, playerBases.Count);
        var move = enemyUnits[currentCreature].Creature.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnits[currentCreature].Creature.Base.Name} used  " +
                                          $"{move.Base.Name} against {playerUnits[currentTarget].Creature.Base.Name}");
        yield return new WaitForSeconds(1f);

        var damageDetails = playerUnits[currentTarget].Creature
            .TakeDamage(move, enemyUnits[currentCreature].Creature);
        yield return playerHuds[currentTarget].UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnits[currentTarget].Creature.Base.Name} fainted");
            yield return new WaitForSeconds(1f);
            
            playerUnits[currentTarget].gameObject.SetActive(false);
            playerHuds[currentTarget].gameObject.SetActive(false);
            if (currentTarget < playerBases.Count - 1)
            {
                BattleUnit item = playerUnits[currentTarget];
                playerUnits.RemoveAt(currentTarget);
                playerUnits.Add(item);
                BattleHud item2 = playerHuds[currentTarget];
                playerHuds.RemoveAt(currentTarget);
                playerHuds.Add(item2);
            } 
            playerBases.RemoveAt(currentTarget);
            
            for (int i = 0; i < order.Count; i++)
            {
                if (order[i] == currentTarget)
                {
                    order.RemoveAt(i);
                }
            }
            for (int i = 0; i < order.Count; i++) {
                if (order[i] > currentTarget && order[i] < enemyBases.Count)
                {
                    order[i] -= 1;
                }
            }

            if (playerBases.Count == 0)
            {
                StartCoroutine(dialogBox.TypeDialog("Oh no, you lose :("));
            }
            else
            {
                StartTurn();
            }
        }
        else
        {
            StartTurn();
        }
    }

    IEnumerator WinFight()
    {
        yield return dialogBox.TypeDialog("Congratulations, you won :)");
        yield return new WaitForSeconds(0.5f);
        GameManager.ExitCombat();
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("A critical hit!");
            yield return new WaitForSeconds(0.5f);
        }
        if (damageDetails.TypeEffectiveness > 1f)
        {
            yield return dialogBox.TypeDialog("It's super effective!");
            yield return new WaitForSeconds(0.5f);
        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return dialogBox.TypeDialog("It's not very effective");
            yield return new WaitForSeconds(0.5f);
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

    IEnumerator Run()
    {
        state = BattleState.Busy;
        yield return dialogBox.TypeDialog("You tried :)");
        yield return new WaitForSeconds(0.3f);
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
        yield return dialogBox.TypeDialog("Choose the enemy to attack");
        yield return new WaitForSeconds(0.5f);
        handR.transform.position = enemyUnits[currentTarget].transform.position + new Vector3(-1.2f,0,0);
        handR.SetActive(true);
        handL.SetActive(false);
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
                StartCoroutine(Run());
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
            if (currentTarget > 0)
            {
                --currentTarget;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentTarget < enemyBases.Count - 1)
            {
                ++currentTarget;
            }
        }
        handR.transform.position = enemyUnits[currentTarget].transform.position + new Vector3(-1.2f,0,0);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            handR.SetActive(false);
            handL.SetActive(false);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
