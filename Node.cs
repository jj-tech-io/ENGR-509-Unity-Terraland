using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbors; //adjacent nodes
    
    public float cost; //distance from start node
    public Node parent; //previous node in the path
    [SerializeField] 
    public float distanceFromStart;
    [SerializeField]
    public float estimatedDistanceToEnd; //g
    public bool visited;
    internal Node previousNode;

    private void Start()
    {
        neighbors = new List<Node>();
    }

    public void AddNeighbor(Node neighbor)
    {
        neighbors.Add(neighbor);
    }
}
