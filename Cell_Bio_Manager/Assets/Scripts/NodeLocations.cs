using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NodeLocations : MonoBehaviour
{
    
    public Node nodeScript; // Prefab of the Node to instantiate

    // Define the area boundaries - found by looking at the scene view edge of inner side of cell wall
    public int xValueMax = 62;
    public int yValueMax = 44;
    public int xValueMin = -62;
    public int yValueMin = -46;

    public int nodeDistance = 3; // Distance between nodes - smaller distance = more nodes

    public List<Node> NodeList = new List<Node>(); // List to hold all spawned nodes - saves time finding nodes later

    private int diagonalDistance; // Calculated distance for diagonal connections between nodes

    void Start()
    {

        // Calculate diagonal distance using Pythagorean theorem
        diagonalDistance = (int)(Mathf.Sqrt((Mathf.Pow(nodeDistance, 2)) * 2)+1);
        //print(diagonalDistance);

        spawnNodes();
        createConnections();
        

    }

    // Spawns nodes in a grid pattern within defined boundaries
    public void spawnNodes()
    {
        // Loop through x and y values to create grid
        for (int i = xValueMin; i <= xValueMax; i += nodeDistance)
        {
            for (int j = yValueMin; j <= yValueMax; j += nodeDistance)
            {
                Vector2 position = new Vector2(i, j); // Define position for node

                // Check for obstacles before spawning node
                if (Physics2D.OverlapCircle(position, 0.4f) != null)
                {
                    // If an obstacle is detected, skip this position
                    if (Physics2D.OverlapCircle(position, 0.4f).gameObject.tag == "Obstacle")
                        continue;
                    
                }
                // Instantiate node and add to NodeList
                Node node = Instantiate(nodeScript, position, Quaternion.identity, transform); //transform as parent
                NodeList.Add(node);
                
            }
        }
    }

    // Creates connections between nodes based on distance
    public void createConnections()
    {
        // Loop through each pair of nodes to check distance
        for (int i=0; i < NodeList.Count; i++)
        {
            for(int j=+1; j < NodeList.Count; j++)
            {
                // Check if nodes are within straight line distance
                if (Vector2.Distance(NodeList[i].transform.position, NodeList[j].transform.position) < diagonalDistance)
                {
                    // If nodes are within diagonal distance, create connection
                    NodeList[i].connectedNodes.Add(NodeList[j]);
                    NodeList[j].connectedNodes.Add(NodeList[i]);
                }
            }
        }

    }

    // Finds and returns the nearest node to a given position
    public Node getNearestNode(Vector2 position)
    {
        Node nearestNode = null; // Variable to hold the nearest node
        float closestDistance = Mathf.Infinity; // Initialize closest distance to infinity

        // Loop through all nodes to find the nearest one
        for (int i = 0; i < NodeList.Count; i++)
        {
            float distance = Vector2.Distance(position, NodeList[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestNode = NodeList[i];
            }
        }
        return nearestNode;
    }


    // Returns a random node from the NodeList
    public Node getRandomNode()
    {
        // Generate a random index within the bounds of the NodeList
        int randomIndex = Random.Range(0, NodeList.Count);
        
        return NodeList[randomIndex];
    }

    // Returns the list of all nodes in the scene
    public List<Node> NodesInScene()
    {
        return NodeList;
    }

    // Visualizes the nodes and their connections in the Unity Editor - not in game
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        
        for (int i = 0; i < NodeList.Count; i++)
        {
            for (int j = 0; j < NodeList[i].connectedNodes.Count; j++)
            {
                Gizmos.DrawLine(NodeList[i].transform.position, NodeList[i].connectedNodes[j].transform.position);
            }
        }
    }

}
