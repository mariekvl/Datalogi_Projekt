using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Node : MonoBehaviour
{
    public Node parent; // Parent node in the pathfinding process
    public List<Node> connectedNodes = new List<Node>(); // List of nodes connected to this node

    public float hValue; // Heuristic value (estimated cost to reach the goal)
    public float gValue; // Cost from the start node to this node

    //public bool isWalkable = true;
    public float FValue() // Total cost function combining gValue and hValue
    {
        return hValue + gValue;
    }

    

    //private void OnDrawGizmos()
    //{
    //    if (!isWalkable)
    //    {
    //        Gizmos.color = Color.white;
    //        Gizmos.DrawSphere(transform.position, .5f);
    //    }
    //}
}
