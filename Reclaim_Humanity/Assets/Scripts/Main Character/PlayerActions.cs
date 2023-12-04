using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float normalSpeed;
    private float currentSpeed = 0f;

    public float NormalSpeed {
        get => normalSpeed;
        set => normalSpeed = value;
    }
    public float CurrentSpeed {
        get => currentSpeed;
        set => currentSpeed = value;
    }

    private Vector2 movingDirection = Vector2.zero;
    [SerializeField] private Vector2 movement;
    private Rigidbody2D rb;
    public Animator animator;
    
    private void Start() {
        // DontDestroyOnLoad(gameObject);
        
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
           animator.SetFloat("Horizontal", movement.x); 
           animator.SetFloat("Vertical", movement.y); 
        }
        
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

	public void Move(InputAction.CallbackContext context) { 
		movingDirection = context.ReadValue<Vector2>(); 
        print(movingDirection);
	}

    public void FixedUpdate() { 
		
        currentSpeed = normalSpeed; //normal movement
        //new method
        //rb.MovePosition(rb.position * movement * normalSpeed * Time.fixedDeltaTime); 
        
        rb.velocity = currentSpeed * movingDirection; //old method
        
	}

    
}
