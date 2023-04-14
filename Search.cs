using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Search : MonoBehaviour
{
    [SerializeField] public JJAgent agent;
    [SerializeField] public List<Node> nodes;
    //get the algorithm dropdown
    [SerializeField] public Dropdown algorithmDropdown;
    [SerializeField]
    public Node startNode;
    [SerializeField]    
    public Node endNode;
    public Node currentNode;
    public Node nextNode;
    public Node previousNode;
    public Node tempNode;
    public bool isSearching = false;
    private enum Algorithm
    {
        //dijsktra's, A*, DFS, BFS
        Dijkstra, AStar, DFS, BFS
    }

    // Start is called before the first frame update
    public void StartSearch()
    {
        isSearching = true;
        //select the algorithm
        Algorithm selectedAlgorithm = (Algorithm)algorithmDropdown.value;
        Debug.Log(selectedAlgorithm);
        switch (selectedAlgorithm)
        {
            case Algorithm.Dijkstra:
                Debug.Log("Dijkstra's");
                Dijkstra();
                break;
            case Algorithm.AStar:
                Debug.Log("A*");
                break;
            case Algorithm.DFS:
                Debug.Log("DFS");
                break;
            case Algorithm.BFS:
                Debug.Log("BFS");
                break;
        }
        //set the start node
        // currentNode = startNode;
        // Debug.Log(currentNode);
        // Debug.Log(currentNode.neighbors);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void InitializeDijkstra()
    {
        foreach (Node node in nodes)
        {
            node.distanceFromStart = Mathf.Infinity;
            node.visited = false;
            node.previousNode = null;
        }

        startNode.distanceFromStart = 0;
    }

    void Dijkstra()
    {
        Debug.Log("Initializing Dijkstra");
        InitializeDijkstra();

        List<Node> unvisitedNodes = new List<Node>(nodes);

        while (unvisitedNodes.Count > 0)
        {
            Debug.Log("Finding closest unvisited node");
            currentNode = FindClosestUnvisitedNode(unvisitedNodes);

            if (currentNode == endNode)
            {
                Debug.Log("Reached end node");
                break;
            }
            if (currentNode == null)
            {
                Debug.Log("No path found");
                break;
            }
            foreach (Node neighbor in currentNode.neighbors)
            {
                Debug.Log("Processing neighbor: " + neighbor.name);
                float tentativeDistance = currentNode.distanceFromStart + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);

                if (tentativeDistance < neighbor.distanceFromStart)
                {
                    neighbor.distanceFromStart = tentativeDistance;
                    neighbor.previousNode = currentNode;
                }
            }

            currentNode.visited = true;
            unvisitedNodes.Remove(currentNode);
            }

            // Retrieve the shortest path
            List<Node> shortestPath = new List<Node>();
            Node pathNode = endNode;

            while (pathNode != null)
            {
                shortestPath.Insert(0, pathNode);
                pathNode = pathNode.previousNode;
            }

            Debug.Log("Shortest Path:");
            foreach (Node node in shortestPath)
            {
                Debug.Log(node.name);
            }

            List<Transform> path = new List<Transform>();

            foreach (Node node in shortestPath)
            {
                path.Add(node.transform);
                Debug.Log("name: " + node.name);
            }
            // agent.waypoints = path;
            // agent.shortestPath = path;
            isSearching = false;
            agent.agentIsWaiting = false;
    }

    Node FindClosestUnvisitedNode(List<Node> unvisitedNodes)
    {
        Node closestNode = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Node node in unvisitedNodes)
        {
            if (node.distanceFromStart < shortestDistance && !node.visited)
            {
                closestNode = node;
                shortestDistance = node.distanceFromStart;
            }
        }

        return closestNode;
    }

    void AStar()
    {
        
    }

    void DFS()
    {
        
    }

    void BFS()
    {
        
    }

}
