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
    public float detectionRadius = 10f; // radius to detect molecules

    public NodeLocations nodeLocations; // reference to NodeLocations script
    public Path pathfinding; // reference to Path script

    //added after hand-in
    public int workerIndex; // index to use with node h and g values in pathfinding

    private List<Node> path;// current path to follow
    public Node currenNode; // current node the worker is on
    private GameObject molecule; // current target molecule

    private Rigidbody2D rb;
    private ActiveRegion activeRegion; // reference to ActiveRegion script
    public List<Molecule> MoleculeData; // reference to Molecule data list 



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

        // get reference to NodeLocations script
        nodeLocations = GameObject.FindGameObjectWithTag("NodeLocations").GetComponent<NodeLocations>();

        // set current node to nearest node at start
        currenNode = nodeLocations.getNearestNode(transform.position);
        activeRegion = GetComponentInChildren<ActiveRegion>();

        // set color to match color of interactable moleule
        changeColor(activeRegion.getLevel());

        // get reference to Path script
        pathfinding = FindAnyObjectByType<Path>();

        addIndexToNodes(); // added after hand-in
    }




    void Update()
    {
        // state machine logic
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

        // state transition logic
        if (currentState != WorkerState.Wander && molecule == null && cooldownTimer == 0f)
        {
            //print("Returning to Wander state");
            currentState = WorkerState.Wander;
            path = null; // reset path when returning to Wander
        }
        else if (currentState != WorkerState.Seek && molecule != null && cooldownTimer == 0f)
        {
            //print("Switching to Seek state");
            currentState = WorkerState.Seek;
            path = null;
        }
        else if (currentState != WorkerState.CoolDown && cooldownTimer > 0f)
        {
            //print("Switching to CoolDown state");
            currentState = WorkerState.CoolDown;
            path = null;
        }
        createPath(); // follow path
    }


    // added after hand-in to initialize g and h values for each node for this worker
    private void addIndexToNodes()
    {
        for (int i = 0; i < nodeLocations.NodeList.Count; i++)
        {
            nodeLocations.NodeList[i].gValues.Add(float.MaxValue); // initialize g value for this worker
            nodeLocations.NodeList[i].hValues.Add(0f); // initialize h value for this worker
        }
    }



    // method for random movement
    void Wander()
    {
        //molecule = null; // clear current target molecule
        //GetComponent<MoleculeBehaviour>().enabled = true; // enable Movement script for wandering
        //GetComponent<MoleculeBehaviour>().randomDirection();

        // generates new path if none exists
        if ( path == null)
        {
            //print("Generating new wander path");
            // generates path to random node
            path = pathfinding.GeneratePath(currenNode, nodeLocations.getRandomNode(),workerIndex); //added workerIndex after hand-in

        }


        // detect nearby colliders within detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        // if more than one collider detected (to exclude self)
        if (colliders.Length > 1)
        {
            
            float closestDistance = Mathf.Infinity; // variable to track closest molecule distance
            GameObject closestMolecule = null; // variable to track closest molecule object
            foreach (Collider2D obj in colliders)
            {
                // check if collider is a molecule
                if (obj.CompareTag("Molecule"))
                {
                    // check if molecule matches activeRegion level
                    int level = activeRegion.getLevel();
                    string moleculeName = obj.gameObject.name.Replace("(Clone)", "").Trim();
                    // if not matching, skip to next collider
                    if (moleculeName != level.ToString())
                        continue;
                    // calculate distance to molecule
                    float distance = Vector2.Distance(transform.position, obj.transform.position);
                    // if this molecule is closer than previous closest, update closest variables
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMolecule = obj.gameObject;
                    }
                }
            }
            // if a closest molecule was found, set as target
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

        // generates new path if none exists
        if (path == null) 
        {
            //print("Generating new seek path");
            // generates path to molecule's nearest node
            path = pathfinding.GeneratePath(currenNode, nodeLocations.getNearestNode(molecule.transform.position), workerIndex); //added workerIndex after hand-in

        }



        // old logic for pathfinding movement

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
        
        //stop movement
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

    // change the color of the worker based on molecule color
    private void changeColor(int level)
    {
        //print("Changing color to " + MoleculeData[level].Color);
        Color color = MoleculeData[level].Color;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    // method to follow path
    void createPath()
    {
        // follow path if it exists
        if (path != null)
        {
            //print("Following path with " + path.Count + " nodes");
            int x = 0; // always target first node in path
            // move towards next node in path
            transform.position = Vector2.MoveTowards(transform.position, path[x].transform.position, speed * Time.deltaTime);
            // rotate towards next node in path
            float angle = Mathf.Atan2((path[x].transform.position - transform.position).y, (path[x].transform.position - transform.position).x) * Mathf.Rad2Deg;
            // apply rotation
            transform.rotation = Quaternion.Euler(0, 0, angle);
            // check if reached next node
            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currenNode = path[x]; // update current node
                path.RemoveAt(x); // remove reached node from path
            }
            // check if path is complete
            if (path.Count == 0)
            {
                //print("Path complete");
                path = null; // reset path
            }
        }
    }
    // visualize detection radius in editor
    void OnDrawGizmosSelected()
    {
        // Draw detection radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
