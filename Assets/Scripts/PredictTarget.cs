using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictTarget : MonoBehaviour
{
    private Transform sphere; // The falling sphere
    public float raycastDistance = 100f; // The maximum distance the raycast will check
    public LayerMask groundMask; // The layer mask for the ground

    void Update()
    {
        if(sphere == null) return;
        // Cast a ray downwards from the sphere's position
        RaycastHit hit;
        if (Physics.Raycast(sphere.position, Vector3.down, out hit, raycastDistance, groundMask))
        {
            // If the raycast hit something, move the landing area to the hit position
            transform.position = hit.point + new Vector3(0f, 0.01f, 0f);
            // Rotate the landing area to match the surface normal
            transform.LookAt(sphere);
        }
    }

    public void SetSphere(Transform sphere) {
        this.sphere = sphere;
    }

    public void DestroyTarget() {
        Destroy(gameObject);
    }

    public void SetScale(float scale) {
        transform.localScale = new Vector3(scale, scale, scale);
    }
}

