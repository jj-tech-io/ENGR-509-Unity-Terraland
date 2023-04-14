using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint[] neighbors;
    public Waypoint[] Neighbors => neighbors;
}
