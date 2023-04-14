using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JJAgent : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private Transform start;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 5.0f;
    public static List<Node> reachedNodes = new List<Node>();
    private int currentWaypointIndex = 0;
    public List<Transform> shortestPath = new List<Transform>();
    public bool agentIsWaiting = true;
    
    private void Start()
    {
        currentWaypointIndex = 0;
        
    }

    private void Update()
    {
        if (agentIsWaiting)
        {
            Debug.Log("Searching");
            return;
        }
        else
        {
            Debug.Log("Not Searching");
        }
        if (shortestPath == null || shortestPath.Count == 0 || currentWaypointIndex >= shortestPath.Count ) return;

        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        Transform currentWaypoint = shortestPath[currentWaypointIndex];
        float distance = Vector3.Distance(transform.position, currentWaypoint.position);

        if (distance <= 0.5f)
        {
            //check if the agent has reached the end node
            if (currentWaypoint == shortestPath[shortestPath.Count - 1])
            {
                Debug.Log("Reached end node");
                //pause the game
                Time.timeScale = 0;
            }
            Debug.Log("Reached waypoint: " + currentWaypointIndex);
            currentWaypointIndex++;

            if (currentWaypointIndex >= shortestPath.Count) return;

            currentWaypoint = shortestPath[currentWaypointIndex];
            reachedNodes.Add(currentWaypoint.GetComponent<Node>());
        }

        Vector3 direction = (currentWaypoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Smoothly rotate the player towards the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
