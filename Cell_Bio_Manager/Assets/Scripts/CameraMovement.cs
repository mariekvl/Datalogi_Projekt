using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    InputAction moveAction;

    public float speed = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveValue = moveAction.ReadValue<Vector2>();
                
       
        transform.position += moveValue * speed;
       
    }
}

