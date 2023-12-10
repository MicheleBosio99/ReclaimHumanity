using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Characteristics")]
    [SerializeField] private List<CreatureBase> enemies;
    [SerializeField] private List<int> levels;// ??
    [SerializeField] private int maxHP;
    private int currentHP;

    [Header("CHASING")] 
    public GameObject player;
    public float speed;
    private float distance;
    private Vector2 direction;
    private SpriteRenderer _spriteR;
    [SerializeField] private bool _isSpriteFrontToRight;

    [Header("FIELD OF VIEW")] [Range(0, 360)]
    public float radius;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    private float angle; // public?
    private bool playerInSight;
    private bool isChasing;

    

    [Header("ENEMY TYPE")] 
    [SerializeField] private EnemyType type;
    private enum EnemyType
    {
        Chasing, // 0: Follows the player --combat when collision
        Spotting, // 1: Looks for the player --combat when on sight
        Detecting // 2: Senses the player --combat when closer than a certain range
    }
    
    [Header("SPAWN SETTINGS")] [SerializeField]
    private GameObject enem;
    [SerializeField] private GameObject spawn;
    private SpawnHandler spawnHandler;
    // target == player

    
    void Start()
    {
        spawnHandler = FindObjectOfType<SpawnHandler>();
        _spriteR = this.GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        //enemyHP.text = "HP: " + currentHP + " / " + maxHP;

        player = GameObject.FindGameObjectWithTag("Player");
        
        EnemyRoutine();
    }

    void Update()
    {

    }

    public void ActivateEnemy()
    {
        enem.SetActive(true);
    }

    public void SpawnPositionEnemy()
    {
        enem.transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
        enem.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) //TEMPORANEO PER KILLARE IL NEMICO E STARTARE LA BATTLE SCENE IN CASO DI "CHASING" ENEMY
    {
        if (collision.CompareTag("Player"))
        {
            enem.SetActive(false);
            spawn.SetActive(false);
            spawnHandler.NumberOfSpawnsActiveDecrement();

            GameManager.enemies = enemies;
            GameManager.enemiesLevels = levels;
            GameManager.EnterCombat();
        }
    }
    
    /*
     * Enemy Type Coroutines
     * Type 0: Chasing --> FOV (30°) + Chasing
     * Type 1: Spotting --> FOV (30° or linear?)
     * Type 2: Detecting --> AOV (360°)
     * ------------
     * FOV = field of view --triangular
     *                     --? linear ?
     * AOV = Area of view --circular
     *     = FOV with angle=360
     */
    
    //Starts a different coroutine for each different type of enemy
    public void EnemyRoutine()
    {
        switch (type)
        {
            case EnemyType.Chasing:
                StartCoroutine(ChaseRoutine());
                //Start Battle done by OnTriggerEnter2D()
                break;
            case EnemyType.Spotting:
                StartCoroutine(FOVRoutine());
                break;
            case EnemyType.Detecting:
                StartCoroutine(AOVRoutine());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private IEnumerator FOVRoutine()
    {
        angle = 30;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            FieldOfViewCheck();
        }
    }
    private IEnumerator AOVRoutine()
    {
        angle = 360;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            FieldOfViewCheck();
        }
    }

    private IEnumerator ChaseRoutine()
    {
        angle = 360;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            FieldOfViewCheck();
            AiChase();
        }
    }
    
    private void FieldOfViewCheck() //Triangular fov (angle based)
    {
        var seen = Physics2D.OverlapCircle(transform.position, radius, targetMask); 
        //returns a collider when the player comes close a certain radius, but the player is not "in sight" yet

        if (seen)
        {
            var target = seen.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            
            if (Vector2.Angle(transform.right, directionToTarget) < angle ||
                Vector2.Angle(-transform.right, directionToTarget) < angle) // angle / 2 ??
            {
                distance = Vector2.Distance(transform.position, player.transform.position);
                
                playerInSight = !Physics2D.Raycast(transform.position, directionToTarget, distance, obstructionMask);
            }
            else playerInSight = false;
        }
        else if (playerInSight)
            playerInSight = false;

        //if (playerInSight)
        //SceneManager.LoadScene("Battle");
    }
    
    void AiChase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        // Wish it worked...
        // playerInSightRange = Physics.CheckSphere(transform.position, viewDistance, thePlayer);

        if (playerInSight)
        {
            // To mirror enemy sprite if it's moving right or left
            _spriteR.flipX = _isSpriteFrontToRight ? !(direction.x >= 0f) : direction.x >= 0f;

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                speed * Time.deltaTime);
            distance = Vector2.Distance(transform.position, player.transform.position);

        }
    }
    
    /* If _spriteR throws exceptions or doesn't work well:
        if (direction.x >= 0f)
            {
                transform.localScale = new Vector2(2, 2);
            }
            else
            {
                transform.localScale = new Vector2(-2, 2);
            }
            
            if (direction.x >= 0f)
            {
                _spriteR.flipX = false;
            }
            else
            {
                _spriteR.flipX = true;
            }
     */
}

    /*private void AreaOfViewCheck() //
    {
        var rangeChecks = Physics2D.OverlapCircle(transform.position, radius, targetMask);
        
        if (rangeChecks)
        {
            Vector2 directionToTarget = (rangeChecks.transform.position - transform.position).normalized;
            distance = Vector2.Distance(transform.position, player.transform.position);

            playerInSight = !Physics2D.Raycast(transform.position, directionToTarget, distance, obstructionMask);
        }
        else if (playerInSight) //se non c'è collisione && playerInSight è true (quindi è stato visto ma non si vede più)
            playerInSight = false;
    }*/