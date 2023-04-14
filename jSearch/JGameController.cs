using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGameController : MonoBehaviour
{
    [SerializeField] public Waypoint[] waypoints;
    [SerializeField] public Waypoint startWaypoint;
    [SerializeField] public Waypoint targetWaypoint;
    [SerializeField] public Mover mover;

    private JDijkstraPathfinder pathfinder;

    private void Start()
    {
        pathfinder = new JDijkstraPathfinder();
        StartCoroutine(FollowShortestPath());
    }

    private IEnumerator FollowShortestPath()
    {
        List<Waypoint> shortestPath = pathfinder.FindShortestPath(startWaypoint, targetWaypoint, waypoints);

        foreach (Waypoint waypoint in shortestPath)
        {
            yield return StartCoroutine(mover.MoveToTarget(waypoint.transform.position));
        }
    }
}