
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public Transform target; // Assign your player's transform to this field.

    public float smoothSpeed = 5.0f; // Adjust the smoothness of camera follow.

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        // transform instantly
        transform.position = targetPosition;
    }
}