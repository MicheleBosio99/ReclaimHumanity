using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float normalSpeed = 8.0f;
    private float currentSpeed = 8.0f;

    public float NormalSpeed {
        get => normalSpeed;
        set => normalSpeed = value;
    }
    public float CurrentSpeed {
        get => currentSpeed;
        set => currentSpeed = value;
    }

    private Vector2 movingDirection = Vector2.zero;
    private Rigidbody2D rb;
    
    private void Start() {
        // DontDestroyOnLoad(gameObject);
        
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

    public void FixedUpdate() { rb.velocity = currentSpeed * movingDirection; }

    public void Move(InputAction.CallbackContext context) { movingDirection = context.ReadValue<Vector2>(); }
}
