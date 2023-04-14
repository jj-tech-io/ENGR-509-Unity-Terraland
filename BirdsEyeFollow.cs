using UnityEngine;

public class BirdsEyeFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // The target object (player) the camera should follow
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10); // The offset of the camera relative to the target

    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}

