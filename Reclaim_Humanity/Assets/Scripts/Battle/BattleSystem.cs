using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BattleState { Start, PlayerAction, PlayerMove, SelectTarget, EnemyMove, Busy, Item, SelectItemTarget }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private List<BattleUnit> playerUnits;
    [SerializeField] private List<BattleHud> playerHuds;
    [SerializeField] private List<BattleUnit> enemyUnits;
    [SerializeField] private List<BattleHud> enemyHuds;

    [SerializeField] private List<CreatureBase> playerBases;
    [SerializeField] private List<int> playerLevels;
    [SerializeField] private List<int> playerHps;
    [SerializeField] private List<Creature> creaturesFainted;
    [SerializeField] private List<int> creaturesFaintedPosition;
    [SerializeField] private List<CreatureBase> enemyBases;
    [SerializeField] private List<int> enemyLevels;
    
    [SerializeField] private BattleDialogBox dialogBox;
    [SerializeField] private GameObject handL;
    [SerializeField] private GameObject handR;
    [SerializeField] private AudioClip Attack_switch;
    [SerializeField] private AudioClip Target_switch;
    [SerializeField] private List<string> curiosities;

    private BattleState state;
    private int currentAction;
    private int currentMove;
    private int currentCreature;
    private int currentTarget;
    private int currentItem;
    
    private List<int> order;

    private List<Move> selectedMoves;
    private List<Enemy> selectedTargets;

    private List<InventoryItem> itemsDropped;
    private List<InventoryItem> itemsToUse;

    private void Start()
    {
        playerBases = GameManager.party;
        playerLevels = GameManager.partyLevels;
        playerHps = GameManager.partyHps;
        enemyBases = GameManager.enemies;
        enemyLevels = GameManager.enemiesLevels;
        creaturesFainted = new List<Creature>();
        creaturesFaintedPosition = new List<int>();
        itemsToUse = new List<InventoryItem>();
        curiosities = new List<string>();
        
        itemsDropped = new List<InventoryItem>();
        foreach (var enemy in GameManager.enemies) {
            var itemDropped = enemy.DroppableObjects[UnityEngine.Random.Range(0, enemy.DroppableObjects.Count)];
            var quantity = UnityEngine.Random.Range(1, 4);
            itemsDropped.Add(itemDropped.ToInventoryItem(quantity));
        }
        
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        dialogBox.EnableCuriosityText(false);
        
        for (int i = 0; i < playerBases.Count; i++)
        {
            playerUnits[i].SetUp(playerBases[i], playerLevels[i], playerHps[i]);
            playerHuds[i].SetData(playerUnits[i].Creature);
        }
        
        for (int i = 0; i < enemyBases.Count; i++)
        {
            enemyUnits[i].SetUp(enemyBases[i], enemyLevels[i]);
            enemyHuds[i].SetData(enemyUnits[i].Creature);
        }
        
        SetUpItems();
        curiosities.Add("Honey never spoils; archaeologists have found pots of honey in ancient " +
                        "Egyptian tombs that are over 3,000 years old and still edible.");
        curiosities.Add("The inventor of the frisbee was turned into a frisbee. Walter Morrison, the inventor of" +
                        " the frisbee, was cremated, and his ashes were turned into a limited edition frisbee.");
        curiosities.Add("The shortest war in history lasted only 38 to 45 minutes between Britain and Zanzibar " +
                        "in 1896.");
        curiosities.Add("Octopuses have three hearts: two pump blood to the gills, and one pumps it to the " +
                        "rest of the body.");
        curiosities.Add("The Great Wall of China is not visible from space with the naked eye, contrary " +
                        "to popular belief.");
        curiosities.Add("The Eiffel Tower can be 15 cm taller during the summer due to the expansion of the " +
                        "iron in the heat.");
        curiosities.Add("Wearing headphones for just an hour increases the bacteria in your ear by 700 times.");
        curiosities.Add("In Switzerland, it is illegal to own just one guinea pig because they are considered " +
                        "social animals.");
        curiosities.Add("The average person will spend six months of their life waiting for red lights " +
                        "to turn green.");
        curiosities.Add("The popular game \"Doom\" (1993) has been installed on various unconventional " +
                        "devices, including pregnancy tests and digital cameras.");
        curiosities.Add(" Bananas contain potassium-40, a radioactive isotope of potassium, making them slightly " +
                        "radioactive. You'd need to eat about 10,000,000 bananas at once to die of radiation poisoning.");
        curiosities.Add("Statistically, you are more likely to be killed by a vending machine than a shark. " +
                        "Vending machines falling over cause a handful of deaths each year.");
        curiosities.Add("You Can't Hum While Holding Your Nose: try it by yourself.");
        
        yield return dialogBox.TypeDialog("Oh no, some enemies :(");
        yield return new WaitForSeconds(0.5f);
        SetOrder();
        StartTurn();
    }
    
    private void SetUpItems()
    {
        foreach (var item in GameManager.ordinaryItemsInInventory)
        {
            if (item.OnCombat)
            {
                itemsToUse.Add(item);
            }
        }
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
            dialogBox.SetItemNames(itemsToUse);
            currentMove = 0;
            currentTarget = 0;
            currentItem = 0;
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
        yield return dialogBox.TypeDialog($"{playerUnits[currentCreature].Creature.Base.CreatureName} used  {move.Base.Name} " +
                                          $"against {enemyUnits[currentTarget].Creature.Base.CreatureName}");
        yield return new WaitForSeconds(1f);
        
        playerUnits[currentCreature].PlayAttackAnimation(enemyUnits[currentTarget].OriginalPos);
        yield return new WaitForSeconds(1f);

        var damageDetails = enemyUnits[currentTarget].Creature.TakeDamage(move, playerUnits[currentCreature].Creature);
        enemyUnits[currentTarget].FlashOnHit();
        yield return enemyHuds[currentTarget].UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnits[currentTarget].Creature.Base.CreatureName} fainted");
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
        yield return dialogBox.TypeDialog($"{enemyUnits[currentCreature].Creature.Base.CreatureName} used  " +
                                          $"{move.Base.Name} against {playerUnits[currentTarget].Creature.Base.CreatureName}");
        yield return new WaitForSeconds(1f);
        
        enemyUnits[currentCreature].PlayAttackAnimation(playerUnits[currentTarget].OriginalPos);
        yield return new WaitForSeconds(1f);

        var damageDetails = playerUnits[currentTarget].Creature.TakeDamage(move, enemyUnits[currentCreature].Creature);
        playerUnits[currentTarget].FlashOnHit2();
        yield return playerHuds[currentTarget].UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnits[currentTarget].Creature.Base.CreatureName} fainted");
            yield return new WaitForSeconds(1f);
            
            creaturesFainted.Add(playerUnits[currentTarget].Creature);
            creaturesFaintedPosition.Add(currentTarget);
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
                if (order[i] > currentTarget)
                {
                    order[i] -= 1;
                }
            }

            if (playerBases.Count == 0)
            {
                StartCoroutine(LoseFight());
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

    IEnumerator WinFight() {
        yield return dialogBox.TypeDialog("Congratulations, you won :)");
        yield return new WaitForSeconds(1f);
        
        GameManager.UpdateCombatItems(itemsToUse);
        
        for (int i = 0; i < itemsDropped.Count; i++)
        {
            yield return dialogBox.TypeDialog(
                $"One enemy dropped {itemsDropped[i].ItemQuantity} {itemsDropped[i].ItemName}");
            yield return new WaitForSeconds(1f);
        }
        List<Creature> creatures = new List<Creature>();
        for (int i = 0; i < playerUnits.Count; i++) {
            creatures.Add(playerUnits[i].Creature);
        }

        if (creaturesFainted.Count > 0) {
            for (int i = creaturesFainted.Count - 1; i >= 0; i--) {
                creatures.Insert(creaturesFaintedPosition[i], creaturesFainted[i]);
                GameManager.party.Insert(creaturesFaintedPosition[i], creaturesFainted[i].Base);
            }
        }

        for (int i = 0; i < GameManager.partyHps.Count; i++) {
            GameManager.partyHps[i] = creatures[i].HP;
            if (creatures[i].HP == 0) {
                GameManager.partyHps[i] = 1;
            }
        }
        
        GameManager.ExitCombat(itemsDropped);
    }

    IEnumerator LoseFight()
    {
        yield return dialogBox.TypeDialog("Oh no, you lose :(");
        yield return new WaitForSeconds(1f);
        List<Creature> creatures = new List<Creature>();
        if (creaturesFainted.Count > 0)
        {
            for (int i = creaturesFainted.Count - 1; i >= 0; i--)
            {
                creatures.Insert(creaturesFaintedPosition[i], creaturesFainted[i]);
                GameManager.party.Insert(creaturesFaintedPosition[i], creaturesFainted[i].Base);
            }
        }
        
        for (int i = 0; i < GameManager.partyHps.Count; i++)
        {
            GameManager.partyHps[i] = creatures[i].HP;
            if (creatures[i].HP == 0)
            {
                GameManager.partyHps[i] = 1;
            }
        }
        GameManager.LoadGame();
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
        {
            yield return dialogBox.TypeDialog("A critical hit! So lucky :)");
            yield return new WaitForSeconds(0.8f);
        }
        if (damageDetails.TypeEffectiveness > 1f)
        {
            yield return dialogBox.TypeDialog("It's super effective!");
            yield return new WaitForSeconds(0.5f);
        }
        else if (damageDetails.TypeEffectiveness < 1f)
        {
            yield return dialogBox.TypeDialog("It's not very effective...");
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator PlayerAction()
    {
        state = BattleState.Busy;
        string text = $"It's {playerUnits[currentCreature].Creature.Base.CreatureName}'s turn, choose an action!";
        yield return dialogBox.TypeDialog(text);
        yield return new WaitForSeconds(0.5f);
        dialogBox.EnableActionSelector(true);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableMoveSelector(false);
        state = BattleState.PlayerAction;
    }

    IEnumerator Curiosity()
    {
        state = BattleState.Busy;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableCuriosityText(true);
        yield return dialogBox.TypeCuriosity(curiosities[UnityEngine.Random.Range(0, curiosities.Count)]);
        yield return new WaitForSeconds(1.4f);
        dialogBox.EnableActionSelector(true);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableCuriosityText(false);
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
    
    void Item()
    {
        if (itemsToUse.Count == 0)
        {
            StartCoroutine(NoItem());
        }
        else
        {
            state = BattleState.Item;
            dialogBox.EnableActionSelector(false);
            dialogBox.EnableDialogText(false);
            dialogBox.EnableItemSelector(true);
        }
    }
    
    IEnumerator NoItem()
    {
        state = BattleState.Busy;
        yield return dialogBox.TypeDialog("You have no item in inventory that can help you :(");
        yield return new WaitForSeconds(0.5f);
        dialogBox.EnableActionSelector(true);
        dialogBox.EnableDialogText(true);
        state = BattleState.PlayerAction;
    }

    IEnumerator PerformItem()
    {
        state = BattleState.Busy;
        yield return dialogBox.TypeDialog($"You give {itemsToUse[currentItem].ItemName} to" +
                                          $" {playerUnits[currentTarget].Creature.Base.CreatureName}");
        yield return new WaitForSeconds(0.5f);
        if (itemsToUse[currentItem].AttributeBoosted == "health")
        {
            playerUnits[currentTarget].Creature.HealHPs(itemsToUse[currentItem].Boost);
            yield return playerHuds[currentTarget].UpdateHP();
            yield return new WaitForSeconds(0.8f);
            yield return dialogBox.TypeDialog($"{playerUnits[currentTarget].Creature.Base.CreatureName} says thank you <3");
            yield return new WaitForSeconds(0.8f);
        }
        if (itemsToUse[currentItem].AttributeBoosted == "attack")
        {
            playerUnits[currentTarget].Creature.AttackBoost += itemsToUse[currentItem].Boost;
            yield return dialogBox.TypeDialog($"{playerUnits[currentTarget].Creature.Base.CreatureName} now feels unbeatable!");
            yield return new WaitForSeconds(0.8f);
        }
        if (itemsToUse[currentItem].AttributeBoosted == "defense")
        {
            playerUnits[currentTarget].Creature.DefenseBoost += itemsToUse[currentItem].Boost;
            yield return dialogBox.TypeDialog($"{playerUnits[currentTarget].Creature.Base.CreatureName} feels protected");
            yield return new WaitForSeconds(0.8f);
        }
        itemsToUse[currentItem].ItemQuantity--;
        if (itemsToUse[currentItem].ItemQuantity == 0)
        {
            itemsToUse.RemoveAt(currentItem);
        }
        StartTurn();
    }

    IEnumerator SelectItemTarget()
    {
        state = BattleState.Busy;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(true);
        dialogBox.EnableItemSelector(false);
        handL.transform.position = playerUnits[currentTarget].transform.position + new Vector3(1f,0,0);
        handL.SetActive(true);
        yield return dialogBox.TypeDialog("Choose the target of the item");
        yield return new WaitForSeconds(0.5f);
        state = BattleState.SelectItemTarget;
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
        else if (state == BattleState.Item)
        {
            HandleItemSelection();
        }
        else if (state == BattleState.SelectItemTarget)
        {
            HandleItemTargetSelection();
        }
        else if (state == BattleState.Busy)
        {
            
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentAction < 3)
            {
                ++currentAction;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentAction > 0)
            {
                --currentAction;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentAction < 2)
            {
                currentAction += 2;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentAction > 1)
            {
                currentAction -= 2;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }
        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                Item();
            }
            else if (currentAction == 2)
            {
                StartCoroutine(Run());
            }
            else if (currentAction == 3)
            {
                StartCoroutine(Curiosity());
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentMove < playerUnits[currentCreature].Creature.Moves.Count - 1)
            {
                ++currentMove;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentMove > 0)
            {
                --currentMove;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }   
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentMove < playerUnits[currentCreature].Creature.Moves.Count - 2)
            {
                currentMove += 2;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }   
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentMove > 1)
            {
                currentMove -= 2;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.B))
        {
            currentMove = 0;
            dialogBox.EnableActionSelector(true);
            dialogBox.EnableDialogText(true);
            dialogBox.EnableMoveSelector(false);
            state = BattleState.PlayerAction;
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnits[currentCreature].Creature.Moves[currentMove]);
        
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SelectTarget());
        }
    }

    void HandleTargetSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentTarget > 0)
            {
                --currentTarget;
                SoundFXManager.instance.PlaySoundFXClip(Target_switch, transform,1f);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentTarget < enemyBases.Count - 1)
            {
                ++currentTarget;
                SoundFXManager.instance.PlaySoundFXClip(Target_switch, transform,1f);
            }
            
        }
        handR.transform.position = enemyUnits[currentTarget].transform.position + new Vector3(-1.2f,0,0);

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            handR.SetActive(false);
            handL.SetActive(false);
            StartCoroutine(PerformPlayerMove());
        }
    }

    void HandleItemSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentItem < itemsToUse.Count - 1)
            {
                ++currentItem;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentItem > 0)
            {
                --currentItem;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }   
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentItem < itemsToUse.Count - 2)
            {
                currentItem += 2;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        }   
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentItem > 1)
            {
                currentItem -= 2;
                SoundFXManager.instance.PlaySoundFXClip(Attack_switch, transform,1f);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.B))
        {
            currentItem = 0;
            dialogBox.EnableActionSelector(true);
            dialogBox.EnableDialogText(true);
            dialogBox.EnableItemSelector(false);
            state = BattleState.PlayerAction;
        }
        dialogBox.UpdateItemSelection(currentItem, itemsToUse[currentItem]);
        
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SelectItemTarget());
        }
    }

    void HandleItemTargetSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentTarget > 0)
            {
                --currentTarget;
                SoundFXManager.instance.PlaySoundFXClip(Target_switch, transform,1f);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentTarget < playerBases.Count - 1)
            {
                ++currentTarget;
                SoundFXManager.instance.PlaySoundFXClip(Target_switch, transform,1f);
            }
            
        }
        handL.transform.position = playerUnits[currentTarget].transform.position + new Vector3(1f,0,0);

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            handL.SetActive(false);
            StartCoroutine(PerformItem());
        }
    }
}
