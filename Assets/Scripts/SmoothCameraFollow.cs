// Sursa script https://github.com/agoodboygames/Smooth-Camera-Follow/blob/main/SmoothCameraFollow.cs
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    private Vector3 _offset;

    [SerializeField] private Transform target;

    [SerializeField] private float smoothTime;

    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake() => _offset = transform.position - target.position;

    private void LateUpdate()
    {
        if(target == null)
            return;
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}