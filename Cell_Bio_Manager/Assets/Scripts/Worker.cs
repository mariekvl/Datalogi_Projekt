using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class Worker : MonoBehaviour
{

    public float speed = 5f;
    public float cooldownDuration = 1f; // duration of cooldown period
    private float cooldownTimer = 0f; // timer for cooldown period
    public float detectionRadius = 10f;

    public NodeLocations nodeLocations;
    public Path pathfinding;
    


    private List<Node> path;
    public Node currenNode;
    private GameObject molecule;

    private Rigidbody2D rb;
    private ActiveRegion activeRegion; // reference to ActiveRegion script
    public List<Molecule> MoleculeData;

        

    // create states
    private enum WorkerState
    {
        Wander,
        Seek,
        CoolDown
    }
    // makes variable type of WorkerState and sets initial state to searching
    private WorkerState currentState = WorkerState.Wander;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        nodeLocations = GameObject.FindGameObjectWithTag("NodeLocations").GetComponent<NodeLocations>();
        
        currenNode = nodeLocations.getNearestNode(transform.position);
        activeRegion = GetComponentInChildren<ActiveRegion>();

        changeColor(activeRegion.getLevel());
        
        pathfinding = FindAnyObjectByType<Path>();

    }




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
            case WorkerState.CoolDown:
                CoolDown();
                break;
        }

        if (currentState != WorkerState.Wander && molecule == null && cooldownTimer == 0f)
        {
            print("Returning to Wander state");
            currentState = WorkerState.Wander;
            path = null; // reset path when returning to Wander
        }
        else if (currentState != WorkerState.Seek && molecule != null && cooldownTimer == 0f)
        {
            print("Switching to Seek state");
            currentState = WorkerState.Seek;
            path = null;
        }
        else if (currentState != WorkerState.CoolDown && cooldownTimer > 0f)
        {
            print("Switching to CoolDown state");
            currentState = WorkerState.CoolDown;
            path = null;
        }
        createPath();
    }




    // method for random movement
    void Wander()
    {
        //molecule = null; // clear current target molecule
        //GetComponent<MoleculeBehaviour>().enabled = true; // enable Movement script for wandering
        //GetComponent<MoleculeBehaviour>().randomDirection();
        

        if ( path == null)
        {
            print("Generating new wander path");
            path = pathfinding.GeneratePath(currenNode, nodeLocations.getRandomNode());
           
        }



        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        // check for molecules within detection radius
        if (colliders.Length > 1)
        {

            float closestDistance = Mathf.Infinity;
            GameObject closestMolecule = null;
            foreach (Collider2D obj in colliders)
            {
                if (obj.CompareTag("Molecule"))
                {
                    int level = activeRegion.getLevel();
                    string moleculeName = obj.gameObject.name.Replace("(Clone)", "").Trim();
                    if (moleculeName != level.ToString())
                        continue;
                    float distance = Vector2.Distance(transform.position, obj.transform.position);
                        
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMolecule = obj.gameObject;
                    }
                }
            }
            if (closestMolecule != null)
            {
                molecule = closestMolecule;
            }
            
            
        }

    }

    // method for moving towards molecule
    void Seek()
    {
        // stops default wander movement
        //GetComponent<MoleculeBehaviour>().enabled = false;
        
       if (path == null) 
       { 
            
            print("Generating new seek path");
            path = pathfinding.GeneratePath(currenNode, nodeLocations.getNearestNode(molecule.transform.position));
            
       }




        //if (path == null)
        //{
        //    Node targetNode = nodeLocations.getNearestNode(molecule.transform.position);
        //    print("Target Node: " + targetNode);
        //    Node currentNode = nodeLocations.getNearestNode(transform.position);
        //    print("Current Node: " + currentNode);
        //    pathfinding = FindAnyObjectByType<Path>();
        //    path = pathfinding.GeneratePath(targetNode,currentNode);
        //    //path = pathfinding.getPath(currentNode, targetNode);
        //}
        //else if (path != null && path.Count > 1)
        //{
        //    if (Vector2.Distance(transform.position, path[0].transform.position) < 0.2f)
        //    {
        //        path.RemoveAt(0);
        //    }
        //    rb.linearVelocity = (path[0].transform.position - transform.position).normalized * speed;
        //}
        //else if (path != null && path.Count == 1)
        //{
        //    // reached final node, move directly towards molecule
        //    Vector2 directionToMolecule = (molecule.transform.position - transform.position).normalized;
        //    rb.linearVelocity = directionToMolecule * speed;
        //    // check if close enough to molecule to transform
        //    if (Vector2.Distance(transform.position, molecule.transform.position) < 0.5f)
        //    {
        //        path = null; // reset path for next Seek
        //    }
        //}





        //// gets target from activeRegion
        //if (activeRegion != null)
        //    //molecule = activeRegion.GetCurrentMolecule();


        //// logic to move towards molecule
        //if (molecule != null)
        //{
        //    Vector2 direction = (molecule.transform.position - transform.position).normalized;
        //    rb.linearVelocity = direction * speed;

        //    // transition to Transform state when close enough to molecule
        //    if (Vector2.Distance(transform.position, molecule.transform.position) < 0.5f)
        //    {
        //        currentState = WorkerState.Transform;
        //    }   
        //}
        //else
        //{
        //    // If no molecule found, return to Wander state
        //    currentState = WorkerState.Wander;
        //}
    }

   

    // method for cooldown period
    void CoolDown()
    {
        // stops default wander movement
        //gameObject.GetComponent<MoleculeBehaviour>().enabled = false;
        rb.linearVelocity = Vector2.zero;
        molecule = null; // clear current target molecule

        // Logic for cooldown period: decrease timer
        cooldownTimer -= Time.deltaTime;
        rb.linearVelocity = Vector2.zero;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = 0f; // reset cooldown time
        }
    }

    private void changeColor(int level)
    {
        print("Changing color to " + MoleculeData[level].Color);
        Color color = MoleculeData[level].Color;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    void createPath()
    {
        if (path != null)
        {
            print("Following path with " + path.Count + " nodes");
            int x = 0;
            transform.position = Vector2.MoveTowards(transform.position, path[x].transform.position, speed * Time.deltaTime);
            float angle = Mathf.Atan2((path[x].transform.position - transform.position).y, (path[x].transform.position - transform.position).x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currenNode = path[x];
                path.RemoveAt(x);
            }
            if (path.Count == 0)
            {
                print("Path complete");
                path = null;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw detection radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
