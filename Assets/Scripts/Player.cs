using System;
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
    [SerializeField] private EnergyBar _energyBar;
    private float _maxHealth;
    private float _maxEnergy;
    private float _currentHealth;
    private float _currentEnergy;
    private bool _canSprint;
    private float _sprintCost = 0.3f; // The amount that is subtracted from the energy bar when sprinting
    private float _sprintHeal = 0.15f; // The amount that is added to the energy bar when not sprinting
    private float _criticalEnergy = 50f; // If you modify this, make sure to modify the gradient for the EnergyBar too (in Unity Editor)
    public Weapon weapon;
    
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        _movementSpeed = 5f;
        _sprintSpeed = 10f;
        _maxHealth = 100f;
        _currentHealth = _maxHealth;
        _maxEnergy = 100f;
        _currentEnergy = _maxEnergy;
        _healthBar.SetMaxHealth(_maxHealth);
        _energyBar.SetMaxEnergy(_maxEnergy);
        _canSprint = true;
    }
    void FixedUpdate() 
    {
        MovePlayer();
    }

    void Update(){
        LookAtMouse();
        _energyBar.SetEnergy(_currentEnergy);
        // Test damage
        if(Input.GetKeyDown(KeyCode.Space)){
            weapon.Fire();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            weapon.Reload();
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
        if (isPlayerMoving() && Input.GetKey(KeyCode.LeftShift) && _canSprint)
        {
            var newEnergy = _currentEnergy - _sprintCost;
            _currentEnergy = newEnergy < 0f ? 0f : newEnergy;
            if(_currentEnergy == 0f)
                _canSprint = false;
            rb.MovePosition(rb.position + GetInputForMovement() * _sprintSpeed * Time.fixedDeltaTime);
        }
        
        else
        {
            var newEnergy = _currentEnergy + _sprintHeal;
            _currentEnergy = newEnergy > _maxEnergy ? _maxEnergy : newEnergy;
            if(_currentEnergy >= _criticalEnergy)
                _canSprint = true;
            rb.MovePosition(rb.position + GetInputForMovement() * _movementSpeed * Time.fixedDeltaTime);
        }
        
    }

    private Vector3 GetInputForMovement()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private bool isPlayerMoving() {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }

    
}
