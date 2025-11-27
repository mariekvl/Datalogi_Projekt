using System;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerMovement : MonoBehaviour
{

    public float speed = 5f;
    public float cooldownDuration = 1f; // duration of cooldown period
    private float cooldownTimer = 0f; // timer for cooldown period
    private GameObject molecule;
    private Rigidbody2D rb;
    private ActiveRegion activeRegion; // reference to ActiveRegion script
    private Movement movement; // reference to Movement script

    // creates states
    private enum WorkerState
    {
        Wander,
        Seek,
        Transform,
        CoolDown
    }
    // makes variable type of WorkerState and sets initial state to searching
    private WorkerState currentState = WorkerState.Wander;


    // method for random movement
    void Wander()
    {
        if (movement != null)
        {
            movement.enabled = true;
        }
    }

    // method for moving towards molecule
    void Seek()
    {
        // stops default wander movement
        if (movement != null)
            movement.enabled = false;

        // gets target from activeRegion
        if (activeRegion != null)
            //molecule = activeRegion.GetCurrentMolecule();


        // logic to move towards molecule
        if (molecule != null)
        {
            Vector2 direction = (molecule.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            // transition to Transform state when close enough to molecule
            if (Vector2.Distance(transform.position, molecule.transform.position) < 0.5f)
            {
                currentState = WorkerState.Transform;
            }   
        }
        else
        {
            // If no molecule found, return to Wander state
            currentState = WorkerState.Wander;
        }
    }

    // method for transforming molecule (activeRegion handling?)
    void Transform()
    {
        // stops default wander movement
        if (movement != null)
            movement.enabled = false;
        
        // Logic to transform molecule
        if (activeRegion != null)
        {
            // trigger ActiveRegion handling
            activeRegion.MoleculeCaught = true;

            // initialize the runtime cooldown timer from the configured duration
            cooldownTimer = cooldownDuration;

            // clear current target so Seek won't immediately retarget the same molecule
            molecule = null;

            // Transition to CoolDown state after transformation
            currentState = WorkerState.CoolDown;
        }
    }

    // method for cooldown period
    void CoolDown()
    {
        // stops default wander movement
        if (movement != null)
            movement.enabled = false;

        // Logic for cooldown period: decrease timer
        cooldownTimer -= Time.deltaTime;
        rb.linearVelocity = Vector2.zero;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = 0f; // reset cooldown time
            currentState = WorkerState.Wander; // return to Wander state
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        // get references to other scripts, if not already assigned
        // Get or add Movement component so Wander can delegate to it
        movement = GetComponent<Movement>();
        activeRegion = GetComponentInChildren<ActiveRegion>();

        

        // only enable movement script in Wander state (null check)
        if (movement != null)
            movement.enabled = (currentState == WorkerState.Wander);

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case WorkerState.Wander:
                Wander();
                break;
            case WorkerState.Seek:
                Seek();
                break;
            case WorkerState.Transform:
                Transform();
                break;
            case WorkerState.CoolDown:
                CoolDown();
                break;
        }

        if (currentState == WorkerState.Wander && activeRegion != null)
        {
            //GameObject targetMolecule = activeRegion.GetCurrentMolecule();
            //if (targetMolecule != null)
            //{
            //    Debug.Log($"{name}: target found = '{targetMolecule.name}' (sceneValid={targetMolecule.scene.IsValid()}) -> switching to Seek");
            //    currentState = WorkerState.Seek;
            //}
        }
    }
}
