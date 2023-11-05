using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour {

    private float normalSpeed = 10.0f;
    [SerializeField] private float currentSpeed;

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
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        rb.velocity = currentSpeed * movingDirection;
    }

    public void Move(InputAction.CallbackContext context) {
        movingDirection = context.ReadValue<Vector2>();
    }
}
