using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    // Enemy Characteristics
    [SerializeField] private string enemyType;
    [SerializeField] private int enemyLevel; // ??
    [SerializeField] private int maxHP;
    private int currentHP;
    
    // CHASING
    public GameObject player;
    public float speed;
    private float distance;
    private Vector2 direction;
    
    // Field of View
    [Range(0, 360)] public float angle;
    public float radius;
    private bool playerInSight;
    private bool isChasing;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    
    // target == player
    
    void Start()
    {
        currentHP = maxHP;
        //enemyHP.text = "HP: " + currentHP + " / " + maxHP;

        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
    
    void Update()
    {
        
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
    private IEnumerator FOVRoutine()
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
    
    

    // TODO enemies drops
}
