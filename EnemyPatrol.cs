using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 5f;
    private bool facingRight = true;

    [Header("Detectores")]
    public Transform groundCheck;
    public float distanceToGround = 1.5f;
    public float distanceToWall = 1.5f;
    public LayerMask groundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.linearVelocity = new Vector2(facingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);
        Vector2 direction = facingRight ? Vector2.right : Vector2.left; 
        RaycastHit2D isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, distanceToGround, groundLayer);
        RaycastHit2D isWallAhead = Physics2D.Raycast(groundCheck.position, direction, distanceToWall, groundLayer); // En vez de transform, se aplicó en el hijo groundCheck. 


        if (isGroundAhead.collider == null || isWallAhead.collider != null)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(groundCheck.position, Vector2.down * distanceToGround);

            Gizmos.color = Color.red;
            Vector2 direction = facingRight ? Vector2.right : Vector2.left;
            Gizmos.DrawRay(groundCheck.position, direction * distanceToWall);
        }
    }
}
