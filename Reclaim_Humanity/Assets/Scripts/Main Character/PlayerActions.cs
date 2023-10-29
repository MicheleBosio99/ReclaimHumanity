using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;
    
    private Vector2 movingDirection = Vector2.zero;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        rb.velocity = speed * movingDirection;
    }

    public void Move(InputAction.CallbackContext context) {
        movingDirection = context.ReadValue<Vector2>();
    }

    public void OpenInventory(InputAction.CallbackContext context) {
        //Open inventory
        if (context.started) { Debug.Log("INVENTORY OPENED"); }
    }
}
