using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Characteristics")]
    [SerializeField] private string enemyType;
    [SerializeField] private int enemyLevel; // ??
    [SerializeField] private int maxHP;
    private int currentHP;

    [Header("CHASING")]
    public GameObject player;
    public float speed;
    private float distance;
    private Vector2 direction;

    [Header("FIELD OF VIEW")]
    [Range(0, 360)] public float angle;
    public float radius;
    private bool playerInSight;
    private bool isChasing;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    [Header("SPAWN SETTINGS")]

    [SerializeField] private GameObject enem;
    [SerializeField] private GameObject spawn;
    private SpawnHandler spawnHandler;
    // target == player

    void Start()
    {
        spawnHandler = FindObjectOfType<SpawnHandler>();
        currentHP = maxHP;
        //enemyHP.text = "HP: " + currentHP + " / " + maxHP;

        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
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
            if (direction.x >= 0f)
            {
                transform.localScale = new Vector2(2, 2);
            }
            else
            {
                transform.localScale = new Vector2(-2, 2);
            }
            
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                speed * Time.deltaTime);
            distance = Vector2.Distance(transform.position, player.transform.position);
            
        }
    }

    // This is like: every 0.02 seconds it checks if the collider detects something
    public IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            AiChase();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider2D rangeChecks = Physics2D.OverlapCircle(transform.position, radius, targetMask);
        
        if (rangeChecks)
        {
            Transform target = rangeChecks.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            

            if (Vector2.Angle(transform.right, directionToTarget) < angle || Vector2.Angle(-transform.right, directionToTarget) < angle) // angle / 2 ??
            {
                distance = Vector2.Distance(transform.position, player.transform.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distance, obstructionMask))
                    playerInSight = true;
                else playerInSight = false;
            }
            else playerInSight = false;
        }
        else if (playerInSight)
            playerInSight = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) //TEMPORANEO PER KILLARE IL NEMICO
    {
        if (collision.tag == "Player")
        {
            enem.SetActive(false);
            spawn.SetActive(false);
            spawnHandler.NumberOfSpawnsActiveDecrement();
        }
    }

    // TODO enemies drops
}
