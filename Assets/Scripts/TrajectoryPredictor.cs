using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Transform))]
public class TrajectoryPredictor : MonoBehaviour
{
    private Camera camera;
    private LineRenderer lineRenderer;
    private Transform player;
    public LayerMask layerMaskToHit;
    public GameObject target;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        player = GetComponent<Transform>();
        camera = Camera.main;
    }

    private List<Vector3> calculateTrajectory(Vector3 start, Vector3 target, float angle) {
        List<Vector3> trajectory = new List<Vector3>();
        float gravity = Physics.gravity.magnitude;
        float angleInRad = angle * Mathf.Deg2Rad;

        Vector3 direction = target - start;
        float horizontalDistance = direction.magnitude;

        // Calculate the velocity needed to throw the object to the target at a specified angle.
        float velocity = horizontalDistance * gravity / (2 * Mathf.Sin(angleInRad) * Mathf.Cos(angleInRad));
        velocity = Mathf.Sqrt(velocity);

        float flightTime = horizontalDistance / (velocity * Mathf.Cos(angleInRad));

        for (float t = 0; t <= flightTime; t += 0.1f)
        {
            Vector3 trajectoryPoint = start + t * velocity * direction.normalized;
            trajectoryPoint.y = start.y + t * velocity * Mathf.Sin(angleInRad) - 0.5f * gravity * t * t;
            trajectory.Add(trajectoryPoint);
        }

        return trajectory;
    }

    public void ShowTrajectory(Vector3 start, Vector3 target, float angle) {
        var trajectoryPoints = calculateTrajectory(start, target, angle);
        lineRenderer.positionCount = trajectoryPoints.Count;

        for (int i = 0; i < trajectoryPoints.Count; i++) {
            lineRenderer.SetPosition(i, trajectoryPoints[i]);
        }
        ShowTarget();
    }

    public bool IsTargetInRange(float minDistance, float maxDistance) {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskToHit)) {
            var distance = Vector3.Distance(hit.point, player.position);
            if(distance > maxDistance || distance < minDistance) 
                return false;
            else
                return true;
        }
        
        return false;
    }

    private void ShowTarget() {
        target.SetActive(true);
    }

    private void HideTarget() {
        target.SetActive(false);
    }

    public void MoveTargetToMouse() {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskToHit)) {
            target.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            target.transform.position = hit.point + hit.normal * 0.01f;
        }
    }

    public Vector3 GetTargetPosition() {
        return target.transform.position;
    }

    public void HideTrajectory() {
        HideTarget();
        lineRenderer.positionCount = 0;
    }
}
