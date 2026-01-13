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

    // List of molecule data for different levels
    public List<Molecule> MoleculeData = new List<Molecule>();

    public PointManager pointManager; // Reference to the PointManager script
    InputAction moveAction; 
    Rigidbody2D rb;
    Vector2 lastInput;
    PlayerInput input;
    public InputActionReference moveActionReference;

    private ActiveRegion activeRegion;
    private int level = 0; // current level of the player
    private int startPrice = 100; // starting price for upgrades 

    // Array of enzyme names corresponding to levels - to be displayed on the enzyme label
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

        pointManager.level = level; // sets the pointManager level to the player's current level
        pointManager.upgradePrice = startPrice; // sets the starting upgrade price

        moveAction = InputSystem.actions.FindAction("Move");
        //print(moveAction);

        // checks if moveAction is null
        if (moveAction == null)
        {
            Debug.LogError("PlayerMovement: Move action not found in Input System.");
        }
        else
        {
            // makes sure moveAction is enabled
            moveAction.Enable();
            //print("Move action enabled.");
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

    // changes the color of the player based on the current level to match the molecule color
    private void changeColor(int level)
    {
        print("Changing color to " + MoleculeData[level].Color);
        Color color = MoleculeData[level].Color;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    // sets the enzyme name on the label based on the current level
    public void setEnzymeName(int level)
    {
        TextMeshPro enzymeLabel = GetComponentInChildren<TextMeshPro>();
        enzymeLabel.text = (string)enzymeNames.GetValue(level);
    }

    // called when the input system previous button is pressed
    public void OnPrevious()
    {
        //print("OnPrevious called");
        if (level > 0)
        {
            level--; // decrease level by 1
            activeRegion.setLevel(level); // set the active region level
            setEnzymeName(level); // update enzyme name
            changeColor(level); // change player color
        }
    }

    // called when the input system next button is pressed
    public void OnNext()
    {
        //print("OnNext called");
        if (level < pointManager.level)
        {
            level++; // increase level by 1
            activeRegion.setLevel(level); // set the active region level

            setEnzymeName(level); // update enzyme name
            changeColor(level); // change player color
        }
    }


    // could have this method to read movement input directly
    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        print("OnMove: " + v);
    }

    
}