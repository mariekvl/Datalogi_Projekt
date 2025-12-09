using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path instance;
    public NodeLocations nodeLocations;

    private void Awake()
    {
        instance = this;
        
    }


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
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();
        foreach (Node node in nodeLocations.NodesInScene())
        {
            node.gValue = float.MaxValue;
            node.hValue = 0;
            node.parent = null;
        }

        start.gValue = 0;
        start.hValue = Vector2.Distance(start.transform.position, end.transform.position);
        openSet.Add(start);
        print("Generating path from " + start.transform.position + " to " + end.transform.position);

        while (openSet.Count > 0)
        {
            int lowestF = default;

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FValue() < openSet[lowestF].FValue())
                {
                    lowestF = i;
                }
            }
            closedSet.Add(openSet[lowestF]);
            Node currentNode = openSet[lowestF];
            openSet.RemoveAt(lowestF);

            if (currentNode == end)
            {
                List<Node> path = new List<Node>();

                path.Insert(0, end);

                while(currentNode != start)
                {
                    currentNode = currentNode.parent;
                    path.Add(currentNode);
                }

                path.Reverse();
                print("Path generated with " + path.Count + " nodes.");
                return path;
            }


            foreach(Node connectedNode in currentNode.connectedNodes)
            {
                if (closedSet.Contains(connectedNode))
                {
                    continue;
                }
                float heldGValue = currentNode.gValue + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if (heldGValue < connectedNode.gValue)
                {
                    connectedNode.parent = currentNode;
                    connectedNode.gValue = heldGValue;
                    currentNode.hValue = Vector2.Distance(connectedNode.transform.position, end.transform.position);
                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                } 
            }
        }


        return null;
    }
}
