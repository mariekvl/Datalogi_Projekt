using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Node : MonoBehaviour
{
    public Node parent;
    public List<Node> connectedNodes = new List<Node>();

    public float hValue;
    public float gValue;

    //public bool isWalkable = true;
    public float FValue()
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
