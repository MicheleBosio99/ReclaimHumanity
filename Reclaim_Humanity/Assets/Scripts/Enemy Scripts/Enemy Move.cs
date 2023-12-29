using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{

    private Rigidbody2D rb;
    private GameObject _mainCharacter;
    [SerializeField] private float speed;
    private Animator _animator;
    private Vector2 directionToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _mainCharacter = GameObject.Find("MainCharacter");
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        directionToPlayer = (_mainCharacter.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * speed;
        _animator.SetFloat("Horizontal", directionToPlayer.x);
        _animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    private void LateUpdate()
    {
        if (rb.velocity.x == 0)
        {
            Debug.Log("Wallo reached");
        }
    }
}
