using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    [SerializeField] private float speed = 420.0f;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb = null;
    private float horizontalInput;
    // Start is called before the first frame update
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        horizontalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(horizontalInput, Input.GetAxisRaw("Vertical"));
        rb.velocity = speed * Time.fixedDeltaTime * movement;

        if (horizontalInput > 0.01f)  //se mi muovo a destra
            transform.localScale = new Vector2(1, 1);
        if (horizontalInput < -0.01f)  //se mi muovo a sinistra
            transform.localScale = new Vector2(-1, 1);
    }
}
