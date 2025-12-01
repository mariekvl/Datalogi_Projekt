using UnityEngine;

public class MoleculeBehaviour : MonoBehaviour
{
  
    public float speed = 5f;

    public float moveY;
    public float moveX;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        randomDirection();
    }

    void randomDirection()
    {
        
        moveX = Random.Range(-1f, 1f);
        moveY = Random.Range(-1f, 1f);

        // Avoid a zero vector and normalize so speed is stable.
        Vector2 dir = new Vector2(moveX, moveY);
        if (dir == Vector2.zero)
            dir = Vector2.up;
        dir.Normalize();

        moveX = dir.x;
        moveY = dir.y;
    }

    private void FixedUpdate()
    {
        // Set velocity directly for deterministic movement and reliable collision.
        Vector2 velocity = new Vector2(moveX, moveY).normalized * speed;

        rb.linearVelocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        // Reflect movement across collision normal for realistic bounce.
        Vector2 move = new Vector2(moveX, moveY);
        if (collision.contacts != null && collision.contacts.Length > 0)
        {
            move = Vector2.Reflect(move, collision.contacts[0].normal).normalized;
        }
        else
        {
            move = -move.normalized; // fallback
        }

        moveX = move.x;
        moveY = move.y;
        
    }
}
