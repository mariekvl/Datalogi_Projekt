using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector2 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(moveHorizontal, moveVertical);
        position += direction * Time.deltaTime * 10;
        transform.position = position;
    }
}
