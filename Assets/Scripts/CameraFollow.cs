
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    public Transform target; // Assign your player's transform to this field.
    private Vector3 offset;

    [SerializeField]
    [Header("Camera Offset")]
    [Tooltip("The offset of the camera from the player's position.")]
    private Vector3 _cameraOffset = new Vector3(0, 12, -4);

    private void Start()
    {
        // offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _cameraOffset;
        // transform instantly
        transform.position = targetPosition;
    }
}