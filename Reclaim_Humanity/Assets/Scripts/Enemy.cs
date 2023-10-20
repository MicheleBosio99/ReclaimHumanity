using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy Characteristics
    [SerializeField] private string enemyType;
    [SerializeField] private int enemyLevel; // ??
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    
    // damaging --for fights
    private bool hit;
    
    // CHASING
    public GameObject player;
    public float speed;
    private float distance;
    private Vector2 direction;
    [SerializeField] private float viewDistance; // useless when FieldOfView will be updated

    // To be used
    public GameObject FieldOfView;
    private bool playerInSight;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        //enemyHP.text = "HP: " + currentHP + " / " + maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
        AiChase();
        
    }

    // TODO - collision with field of view (red triangle) start chasing phase
    void AiChase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        // Wish it worked...
        // playerInSightRange = Physics.CheckSphere(transform.position, viewDistance, thePlayer);

        if (distance < 3f)
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
    
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    
}
