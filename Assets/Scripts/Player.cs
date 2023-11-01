using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float _movementSpeed;
    private float _sprintSpeed;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        _movementSpeed = 5f;
        _sprintSpeed = 10f;
    }

    void FixedUpdate() 
    {
        MovePlayer();
    }

    void Update(){
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 mousePosition = Input.mousePosition;

        Vector3 mouseDirection = mousePosition - screenCenter;
        mouseDirection.Normalize();

        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }
    
    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(rb.position + GetInputForMovement() * _sprintSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + GetInputForMovement() * _movementSpeed * Time.fixedDeltaTime);
        }
        
    }

    private Vector3 GetInputForMovement()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    
}
