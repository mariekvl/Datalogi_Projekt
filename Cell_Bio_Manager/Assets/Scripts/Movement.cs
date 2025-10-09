using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 200f;
   

    public float moveHorizontal;
    public float moveVertical;

    private Rigidbody2D rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Access player's Rigidbody.
        moveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2 = transform.right * moveVertical;
        Vector2 direction = transform.up * moveHorizontal;
        print(direction);
        Vector2 movement = (direction + vector2) * speed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
        print(rb.position);

    }

    void moveDirection()
    {
        moveVertical = Random.Range(-1f, 1f);
        if (moveVertical > 0)
        {
            moveVertical = 1f;
        }
        else if (moveVertical < 0)
        {
            moveVertical = -1f;
        }
        
        moveHorizontal = Random.Range(-1f, 1f);
        if (moveHorizontal > 0)
        {
            moveHorizontal = 1f;
        }
        else if (moveHorizontal < 0)
        {
            moveHorizontal = -1f;
        }
       
        
    }
}
