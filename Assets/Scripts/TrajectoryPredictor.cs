using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Transform))]
public class TrajectoryPredictor : MonoBehaviour
{
    private Camera mainCamera;
    private LineRenderer lineRenderer;
    private Transform player;
    public LayerMask layerMaskToHit;
    public LayerMask TrajectoryLayerMask;
    public GameObject target;
    public int maxPoints = 50;
    public Rigidbody grenadeRb;
    public float increment = 0.025f;
    public float rayOverlap = 1.1f;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        player = GetComponent<Transform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

     private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
    {
        lineRenderer.positionCount = count;
        lineRenderer.SetPosition(pointPos.point, pointPos.pos);
    }

    public void ShowTrajectory(Vector3 start, RaycastHit hit, float angle) {
        var target = hit.point;
        var position = start;
        var velocity = CalcGrenadeVelocity(start, target, angle);
        Vector3 nextPosition;
        float overlap;

        UpdateLineRender(maxPoints, (0, start));

        for (int i = 1; i < maxPoints; i++)
        {
            // Estimate velocity and update next predicted position
            velocity = CalculateNewVelocity(velocity, grenadeRb.drag, increment);
            nextPosition = position + velocity * increment;

            // Overlap our rays by small margin to ensure we never miss a surface
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            //When hitting a surface we want to show the surface marker and stop updating our line
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit trajectoryHit, overlap, TrajectoryLayerMask))
            {
                UpdateLineRender(i, (i - 1, trajectoryHit.point));
                ShowTarget();
                MoveTargetToHitPoint(trajectoryHit);
                break;
            }

            //If nothing is hit, continue rendering the arc without a visual marker
            HideTarget();
            position = nextPosition;
            UpdateLineRender(maxPoints, (i, position)); //Unneccesary to set count here, but not harmful
        }
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }

    public Vector3 CalcGrenadeVelocity(Vector3 source, Vector3 target, float angle) {
        Vector3 direction = target - source;            				
		float h = direction.y;                                        
		direction.y = 0;                                               
		float distance = direction.magnitude;                        
		float a = angle * Mathf.Deg2Rad;                                
		direction.y = distance * Mathf.Tan(a);                            
		distance += h/Mathf.Tan(a);
        distance = Mathf.Abs(distance);                                      

		// calculate velocity
		float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2*a));
        var finalVelocity = velocity * direction.normalized;
		return finalVelocity;
    }

    public bool IsTargetInRange(float minDistance, float maxDistance) {
        var hit = GetMouseHit();
        var distance = Vector3.Distance(player.position, hit.point);
        return distance >= minDistance && distance <= maxDistance;
    }

    private void ShowTarget() {
        target.SetActive(true);
    }

    private void HideTarget() {
        target.SetActive(false);
    }

    public void MoveTargetToHitPoint(RaycastHit hit) {
        target.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        target.transform.position = hit.point + hit.normal * 0.01f;
    }

    public RaycastHit GetMouseHit() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskToHit);
        return hit;
    }

    public void HideTrajectory() {
        HideTarget();
        lineRenderer.positionCount = 0;
    }
}
