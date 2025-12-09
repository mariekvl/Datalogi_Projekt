using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NodeLocations : MonoBehaviour
{
    public GameObject Node;
    public Node nodeScript;

    public int xValueMax = 62;
    public int yValueMax = 44;
    public int xValueMin = -62;
    public int yValueMin = -46;

    public int nodeDistance = 3;

    public List<Node> NodeList = new List<Node>();

    private int diagonalDistance;

    void Start()
    {
        

        diagonalDistance = (int)(Mathf.Sqrt((Mathf.Pow(nodeDistance, 2)) * 2)+1);
        //print(diagonalDistance);

        spawnNodes();
        createConnections();
        

    }

    public void spawnNodes()
    {
        for (int i = xValueMin; i <= xValueMax; i += nodeDistance)
        {
            for (int j = yValueMin; j <= yValueMax; j += nodeDistance)
            {
                Vector2 position = new Vector2(i, j);

                if (Physics2D.OverlapCircle(position, 0.4f) != null)
                {
                    if (Physics2D.OverlapCircle(position, 0.4f).gameObject.tag == "Obstacle")
                        continue;
                    
                }
                
                Node node = Instantiate(nodeScript, position, Quaternion.identity, transform);
                NodeList.Add(node);
                
            }
        }
    }

    public void createConnections()
    {
        
        for (int i=0; i < NodeList.Count; i++)
        {
            for(int j=+1; j < NodeList.Count; j++)
            {
                
                if (Vector2.Distance(NodeList[i].transform.position, NodeList[j].transform.position) < diagonalDistance)
                {

                    NodeList[i].connectedNodes.Add(NodeList[j]);
                    NodeList[j].connectedNodes.Add(NodeList[i]);
                }
            }
        }

    }

    public Node getNearestNode(Vector2 position)
    {
        Node nearestNode = null;
        float closestDistance = Mathf.Infinity;
        
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



    public Node getRandomNode()
    {
        
        int randomIndex = Random.Range(0, NodeList.Count);
        
        return NodeList[randomIndex];
    }

    public List<Node> NodesInScene()
    {
        return NodeList;
    }

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
