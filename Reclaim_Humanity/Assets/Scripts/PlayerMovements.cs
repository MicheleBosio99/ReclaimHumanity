using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour {

    [SerializeField] private float speed = 420.0f;
    private Vector2 movement = Vector2.zero;
    private Rigidbody2D rb = null;
    // Start is called before the first frame update
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = speed * Time.fixedDeltaTime * movement;
    }
}
