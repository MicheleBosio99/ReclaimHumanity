using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    private void Start() { rb = GetComponent<Rigidbody2D>(); }

    private void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

	public void Move(InputAction.CallbackContext context) { movingDirection = context.ReadValue<Vector2>(); }

    public void FixedUpdate() { 
		
        currentSpeed = normalSpeed;

        rb.velocity = currentSpeed * movingDirection * Time.fixedDeltaTime;
        rb.position = rb.position + movement * currentSpeed * Time.fixedDeltaTime;

    }

    public void WalkPlayerToPosition(Vector2 endPos) { StartCoroutine(WalkPlayer(endPos)); }

    private IEnumerator WalkPlayer(Vector2 endPos) {
        var initSpeed = currentSpeed; currentSpeed = normalSpeed;

        var position = gameObject.transform.position;
        var direction = ((Vector3) endPos - position).normalized;
        var distance = Vector3.Distance(position, endPos);

        while (distance > 0.1f) {
            movingDirection = direction;
            distance = Vector3.Distance(gameObject.transform.position, endPos);
            yield return null;
        }
        currentSpeed = initSpeed;
        movingDirection = Vector2.zero;
    }
}
