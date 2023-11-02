using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float _movementSpeed;
    private float _sprintSpeed;
    [SerializeField] private HealthBar _healthBar;
    private float _maxHealth;
    private float _currentHealth;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        _movementSpeed = 5f;
        _sprintSpeed = 10f;
        _maxHealth = 100f;
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }
    void FixedUpdate() 
    {
        MovePlayer();
    }

    void Update(){
        LookAtMouse();
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20f);
        }
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
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
