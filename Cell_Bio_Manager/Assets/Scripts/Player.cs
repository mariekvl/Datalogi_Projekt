using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    // The speed at which the player moves
    public float speed = 5f;

    public List<Molecule> MoleculeData = new List<Molecule>();

    public PointManager pointManager;
    InputAction moveAction;
    Rigidbody2D rb;
    Vector2 lastInput;
    PlayerInput input;
    public InputActionReference moveActionReference;

    private ActiveRegion activeRegion;
    private int level = 0;
    private int startPrice = 100;


    private Array enzymeNames = new string[]
    {
        "Hexokinase",
        "Phosphoglucose Isomerase",
        "Phosphofructokinase",
        "Aldolase",
        "Triosephosphate Isomerase",
        "Glyceraldehyde 3-phosphate Dehydrogenase",
        "Phosphoglycerate Kinase",
        "Phosphoglycerate Mutase",
        "Enolase",
        "Pyruvate Kinase"
    };


    void Start()
    {

        input = GetComponent<PlayerInput>();
        input.ActivateInput();
        rb = GetComponent<Rigidbody2D>();
        activeRegion = GetComponentInChildren<ActiveRegion>();
        changeColor(level);

        moveAction = moveActionReference.action;

        pointManager.level = level;
        pointManager.upgradePrice = startPrice;

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


    private void changeColor(int level)
    {
        print("Changing color to " + MoleculeData[level].Color);
        Color color = MoleculeData[level].Color;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    public void setEnzymeName(int level)
    {
        TextMeshPro enzymeLabel = GetComponentInChildren<TextMeshPro>();
        enzymeLabel.text = (string)enzymeNames.GetValue(level);
    }

    public void OnPrevious()
    {
        print("OnPrevious called");
        if (level > 0)
        {
            level--;
            activeRegion.setLevel(level);
            setEnzymeName(level);
            changeColor(level);
        }
    }

    public void OnNext()
    {
        print("OnNext called");
        if (level < pointManager.level)
        {
            level++;
            activeRegion.setLevel(level);

            setEnzymeName(level);
            changeColor(level);
        }
    }



    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        print("OnMove: " + v);
    }

    
}