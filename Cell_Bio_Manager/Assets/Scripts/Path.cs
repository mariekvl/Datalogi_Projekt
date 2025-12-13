using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    // Singleton instance - meaning there is only one Path object in the scene
    public static Path instance;
    public NodeLocations nodeLocations;

    private void Awake()
    {

        instance = this;
        
    }


    //non-functional pathfinding method - it crashes the game when used
    public List<Node> getPath(Node start, Node goal)
    {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> pathList = new List<Node>();

        Node currentNode = start;

        foreach (Node node in nodeLocations.NodesInScene())
        {
            node.gValue = float.MaxValue;
            node.hValue = 0;
            node.parent = null;
        }

        openList.Add(currentNode);
        currentNode.gValue = 0;
        currentNode.hValue = Vector2.Distance(currentNode.transform.position, goal.transform.position);

        if (currentNode == goal)
        {
            Node pathNode = goal;
            while (pathNode != null)
            {
                pathList.Add(pathNode);
                pathNode = pathNode.parent;
            }
            pathList.Reverse();
            return pathList;
        }


        while (currentNode != goal)
        {
            for (int i = 0; i < currentNode.connectedNodes.Count; i++)
            {
                //if (!currentNode.connectedNodes[i].isWalkable || closedList.Contains(currentNode.connectedNodes[i]))
                //{
                //    continue;
                //}
                Node connectedNode = currentNode.connectedNodes[i];
                connectedNode.gValue = currentNode.gValue + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);
                connectedNode.hValue = Vector2.Distance(connectedNode.transform.position, goal.transform.position);
                connectedNode.parent = currentNode;
            }


            openList.AddRange(currentNode.connectedNodes);

            openList.Sort((nodeA, nodeB) => nodeA.FValue().CompareTo(nodeB.FValue()));
            currentNode = openList[0];
            openList.RemoveAt(0);
            closedList.Add(currentNode);
        }


        

        

        return pathList;
    }






    public List<Node> GeneratePath(Node start, Node end)
    {
        List<Node> openSet = new List<Node>(); //nodes to be evaluated
        List<Node> closedSet = new List<Node>(); //nodes already evaluated - to prevent re-evaluation

        //reset all nodes' g and h values and parent
        foreach (Node node in nodeLocations.NodesInScene())
        {
            node.gValue = float.MaxValue; 
            node.hValue = 0;
            node.parent = null;
        }

        //initialize start node
        start.gValue = 0;
        start.hValue = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);
        //print("Generating path from " + start.transform.position + " to " + end.transform.position);

        //main A* loop
        while (openSet.Count > 0)
        {
            //index of node in openSet with lowest F value
            int lowestF = default; //default to 0

            //find node in openSet with lowest F value
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FValue() < openSet[lowestF].FValue())
                {
                    lowestF = i;
                }
            }
            //move current node from openSet to closedSet
            closedSet.Add(openSet[lowestF]);
            Node currentNode = openSet[lowestF]; //node with lowest F value
            openSet.RemoveAt(lowestF);

            //check if we reached the end node to prevent unnecessary calculations
            if (currentNode == end)
            {
                List<Node> path = new List<Node>(); //final path

                path.Insert(0, end); //add end node to path

                // trace back to start node using parent references
                while (currentNode != start)
                {
                    currentNode = currentNode.parent;
                    path.Add(currentNode);
                }

                path.Reverse(); //reverse path to get correct order from start to end
                //print("Path generated with " + path.Count + " nodes.");
                return path;
            }

            //evaluate each node connected to the current node
            foreach (Node connectedNode in currentNode.connectedNodes)
            {
                //skip if node is already evaluated
                if (closedSet.Contains(connectedNode))
                {
                    continue;
                }
                //calculate tentative g value
                float heldGValue = currentNode.gValue + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                //if new path to connected node is shorter, update its g value and parent
                if (heldGValue < connectedNode.gValue)
                {
                    connectedNode.parent = currentNode;
                    connectedNode.gValue = heldGValue;
                    //recalculate h value
                    currentNode.hValue = Vector2.Distance(connectedNode.transform.position, end.transform.position);
                    //add connected node to openSet if not already present
                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                } 
            }
        }

        return null; //return null if no path found
    }
}
