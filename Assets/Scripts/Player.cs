using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float movementSpeed;

    public Transform gunSpawnPoint;
    public GameObject gunPrefab;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        movementSpeed = 5f;
    }

    void FixedUpdate() 
    {
        LookAtMouse();
        MovePlayer();
    }

    private void LookAtMouse()
    {
        Vector3 mouseDirection = GetMouseDirection();
        transform.rotation = Quaternion.LookRotation(mouseDirection - transform.position);
    }

    private Vector3 GetMouseDirection() {
        Vector3 mouseDirection = Input.mousePosition;
        mouseDirection.z = 7.5f;
        return Camera.main.ScreenToWorldPoint(mouseDirection);
    }
    
    private void MovePlayer()
    {
        rb.MovePosition(rb.position + GetInputForMovement() * movementSpeed * Time.fixedDeltaTime);
    }

    private Vector3 GetInputForMovement()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
} 
