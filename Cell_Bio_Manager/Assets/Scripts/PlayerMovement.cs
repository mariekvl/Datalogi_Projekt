using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    // The speed at which the player moves
    public float speed = 5f; 

    InputAction moveAction;
    Rigidbody2D rb;
    Vector2 lastInput;
    PlayerInput input;
    public InputActionReference moveActionReference;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.actions.FindActionMap("Player").Enable();
        input.enabled = true;
        
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        moveAction = moveActionReference.action;


        moveAction = InputSystem.actions.FindAction("Move");
        print(moveAction);

        // checks if moveAction is null
        if (moveAction == null)
        {
            Debug.LogError("PlayerMovement: Move action not found in Input System.");
        }
        else
        {
            // makes sure moveAction is enabled
            moveAction.Enable();
            print("Move action enabled.");
        }

    }

    void FixedUpdate()
    {

        // ensures there is no errors if no input is found
        if (moveAction == null) return;

        // reads the value of the move action (input)
        Vector2 moveValue = moveAction.ReadValue<Vector2>();


        if (moveValue != Vector2.zero)
        {
            lastInput = moveValue;
            print(moveValue);
        }

        // uses moveValue (input value) to compute delta
        Vector2 delta = moveValue * speed * Time.fixedDeltaTime;

        // makes sure to use rigidbody physics to move
        if (rb != null)
        {
            rb.MovePosition(rb.position + delta);

            if (lastInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(lastInput.y, lastInput.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
            }
        }
        else
        {
            transform.position += (Vector3)delta;

            if (lastInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(lastInput.y, lastInput.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }


    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        print("OnMove: " + v);
    }

    
}