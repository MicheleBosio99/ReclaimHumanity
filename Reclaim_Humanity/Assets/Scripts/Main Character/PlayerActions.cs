using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float normalSpeed;
    [SerializeField] private float currentSpeed;

    public float NormalSpeed {
        get => normalSpeed;
        set => normalSpeed = value;
    }
    public float CurrentSpeed {
        get => currentSpeed;
        set => currentSpeed = value;
    }

    public Vector2 movingDirection;
    [SerializeField] private Vector2 movement;
    private Rigidbody2D rb;
    public Animator animator;
    
    public AudioClip MoveClip;
    
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

    private void OnEnable() { currentSpeed = normalSpeed; }

    private void OnDisable() { rb.velocity = Vector2.zero; }

    private void Update() {
        movement = movingDirection;

        if (movement != Vector2.zero){
            
            if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
            {
           	    movement.y=0;
			    animator.SetFloat("Horizontal", movement.x); 
			    animator.SetFloat("Vertical", movement.y); 
            }
		    else {
			    movement.x=0;
			    animator.SetFloat("Horizontal", movement.x); 
			    animator.SetFloat("Vertical", movement.y); 
		    }
            
            //play the sound for move
            //SoundFXManager.instance.PlayMovementFXClip(MoveClip, transform,1f);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    public void OnMove(InputAction.CallbackContext context) { movingDirection = context.ReadValue<Vector2>(); }

    public void FixedUpdate() { rb.velocity = currentSpeed * movingDirection; }
    
    public void MovePlayer(Vector2 endPosition) {
        movement.x = 0.0f;
        movement.y = 1.0f;
    }

    public void ChangePlayerPosition(Vector2 endPosition) { gameObject.transform.position = endPosition; }

    public void WalkPlayerToPosition(Vector2 endPos) { StartCoroutine(WalkPlayer(endPos)); }
    
    public IEnumerator WalkPlayer(Vector2 endPos) {
        var oldSpeed = currentSpeed; currentSpeed = normalSpeed;
        
        var position = gameObject.transform.position;
        var distance = Vector3.Distance(position, endPos);
    
        while (distance > 0.1f) {
            movingDirection = ((Vector3) endPos - position).normalized;
            var step = currentSpeed * Time.deltaTime;
            
            Transform transform1;
            (transform1 = transform).position = Vector3.MoveTowards(transform.position, endPos, step);
            distance = Vector3.Distance(transform1.position, endPos);
            
            yield return null;
        }
        movingDirection = Vector2.zero;
        
        currentSpeed = oldSpeed;
    }
}
