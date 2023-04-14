using System.Collections.Generic;
using UnityEngine;

public class JDijkstraPathfinder
{
    public List<Waypoint> FindShortestPath(Waypoint start, Waypoint target, Waypoint[] waypoints)
    {
        Dictionary<Waypoint, float> distances = new Dictionary<Waypoint, float>();
        Dictionary<Waypoint, Waypoint> previous = new Dictionary<Waypoint, Waypoint>();
        List<Waypoint> unvisited = new List<Waypoint>(waypoints);

        foreach (Waypoint waypoint in waypoints)
        {
            distances[waypoint] = float.MaxValue;
        }

        distances[start] = 0;

        while (unvisited.Count > 0)
        {
            Waypoint current = GetWaypointWithShortestDistance(distances, unvisited);
            unvisited.Remove(current);

            if (current == target)
            {
                return ReconstructPath(previous, current);
            }

            foreach (Waypoint neighbor in current.Neighbors)
            {
                float tentativeDistance = distances[current] + Vector3.Distance(current.transform.position, neighbor.transform.position);

                if (tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    previous[neighbor] = current;
                }
            }
        }

        return null;
    }

    private Waypoint GetWaypointWithShortestDistance(Dictionary<Waypoint, float> distances, List<Waypoint> unvisited)
    {
        Waypoint minWaypoint = null;
        float minDistance = float.MaxValue;

        foreach (Waypoint waypoint in unvisited)
        {
            if (distances[waypoint] < minDistance)
            {
                minDistance = distances[waypoint];
                minWaypoint = waypoint;
            }
        }

        return minWaypoint;
    }

    private List<Waypoint> ReconstructPath(Dictionary<Waypoint, Waypoint> previous, Waypoint current)
    {
        List<Waypoint> path = new List<Waypoint> { current };

        while (previous.ContainsKey(current))
        {
            current = previous[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}
