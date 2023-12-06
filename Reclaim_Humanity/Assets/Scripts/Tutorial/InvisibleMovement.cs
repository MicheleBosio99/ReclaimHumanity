using Cinemachine;
using UnityEngine;

public class InvisibleMovement : MonoBehaviour
{
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float speed;
    [SerializeField] private CinemachineVirtualCamera _camera;
    
    private Rigidbody2D rb;
    private bool move;
    private const float stoppingDistance = 0.1f;
    private Transform previousFollow;

    private void Start() {
        GameObject go;
        rb = (go = gameObject).GetComponent<Rigidbody2D>();
        previousFollow = _camera.m_Follow;
        _camera.Follow = go.transform;
        move = true;
    }

    private void Update() {
        if (!move) { return; }
        rb.velocity = (targetPosition - (Vector2) transform.position).normalized * speed;
    }

    private void FixedUpdate() {
        if (!move) return;
        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (!(distanceToTarget < stoppingDistance)) return;
        EndMovement();
    }
    
    private void EndMovement() {
        rb.velocity = Vector3.zero;
        Destroy(gameObject);
        _camera.Follow = previousFollow;
    }
}
