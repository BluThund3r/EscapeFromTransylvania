using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrajectoryPredictor))]
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
    private int grenadeNumber = 0;
    public int maxGrenadeNumber = 10;
    private int attackSelection = 0;
    public float minGrenadeRange = 2f;
    public float maxGrenadeRange = 15f;
    private TrajectoryPredictor trajectoryPredictor;
    public GrenadeCounter GrenadeCounter;
    public Weapon weapon;
    private GameObject weaponObject;
    public GameObject WeaponSupplierPrefab;
    public GameObject GrenadePrefab;
    public float grenadeThrowAngle = 30f;
    public float timeOfGrenadeFlight = 2f;
    private bool grenadeThrown = false;
    public float GrenadeCooldown = 4f;
    
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
        // weaponObject = GetWeaponObject();
        // weapon = weaponObject.GetComponent<Weapon>();
        // DropWeapon(false);
        GrenadeCounter.SetActive(false);
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        Cursor.visible = false;
        Cursor.visible = true;
    }
    void FixedUpdate() 
    {
        MovePlayer();
    }

    private GameObject GetWeaponObject() {
        for(int i = 0; i < transform.childCount; i++) {
            if(transform.GetChild(i).CompareTag("Weapon")) {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    private Vector3 GetGrenadeSpawnPoint() {
        return transform.position + transform.forward * transform.localScale.x;
    }

    void Update() {
        LookAtMouse();
        _energyBar.SetEnergy(_currentEnergy);
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && this.hasWeapon()) {
            attackSelection = 1;
            weapon.FocusBulletCount();
            GrenadeCounter.Unfocus();
            trajectoryPredictor.HideTrajectory();
        }
            
        else if(Input.GetKeyDown(KeyCode.Alpha2) && this.hasGrenades()) {
            attackSelection = 2;
            if(hasWeapon())
                weapon.UnfocusBulletCount();
            GrenadeCounter.Focus();
        }
        else if(!hasWeapon() && !hasGrenades())
            attackSelection = 0;


        if(attackSelection == 0) {
            Cursor.visible = true;
            trajectoryPredictor.HideTrajectory();
        }     
        
        if(attackSelection == 1 && this.hasWeapon()) {
            Cursor.visible = true;
            if(Input.GetMouseButtonDown(0))
                weapon.Fire();

            else if(Input.GetKeyDown(KeyCode.R))
                weapon.Reload();

            else if (Input.GetKeyDown(KeyCode.Q))
                DropWeapon();
        }

        if(attackSelection == 2 && hasGrenades()) { 
            if(trajectoryPredictor.IsTargetInRange(minGrenadeRange, maxGrenadeRange) && !grenadeThrown) {
                Cursor.visible = false;
                trajectoryPredictor.ShowTrajectory(transform.position, trajectoryPredictor.GetMouseHit(), grenadeThrowAngle);
            } else {
                Cursor.visible = true;
                trajectoryPredictor.HideTrajectory();
            }
            if(Input.GetMouseButtonDown((int)MouseButton.Left) && 
                trajectoryPredictor.IsTargetInRange(minGrenadeRange, maxGrenadeRange) &&
                !grenadeThrown)
                StartCoroutine(ThrowGrenade(grenadeThrowAngle));
        }
    }

    private bool hasGrenades() {
        return grenadeNumber > 0;
    }

    private IEnumerator ThrowGrenade(float angleDeg) {
        grenadeThrown = true;
        var grenadeSpawnPoint = GetGrenadeSpawnPoint();
        var grenadeRb = Instantiate(GrenadePrefab, grenadeSpawnPoint, Quaternion.identity).GetComponent<Rigidbody>();
        grenadeRb.AddForce(trajectoryPredictor.CalcGrenadeVelocity(
            grenadeSpawnPoint, 
            trajectoryPredictor.GetMouseHit().point, 
            angleDeg), ForceMode.Impulse);

        grenadeNumber--;

        GrenadeCounter.RefreshGrenadeCounter(grenadeNumber, maxGrenadeNumber);

        if(!hasGrenades()) {
            GrenadeCounter.SetActive(false);
            attackSelection = 0;
        }

        
        yield return new WaitForSeconds(GrenadeCooldown);
        grenadeThrown = false;
        yield break;
    }

    public bool PickUpGrenade() {
        if(grenadeNumber == maxGrenadeNumber)
            return false;
        
        grenadeNumber++;
        GrenadeCounter.RefreshGrenadeCounter(grenadeNumber, maxGrenadeNumber);
        if(grenadeNumber == 1) {
            GrenadeCounter.SetActive(true);
            GrenadeCounter.Unfocus();
        }
            
        
        return true;
    }

    void LateUpdate(){
        // make sure player stays on the ground
        if(transform.position.y < 0.995f)
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }
    private bool DropWeapon(bool createSupplier = true) {
        if(!hasWeapon())
            return false;

        var maxBulletsLoaded = weapon._maxBulletsLoaded;
        var maxBulletsMagazine = weapon._maxBulletsMagazine;
        var bulletsLoaded = weapon._bulletsLoaded;
        var bulletsMagazine = weapon._bulletsMagazine;
        weapon.MakeBulletCountDisable();
        weaponObject.transform.parent = null;
        DestroyImmediate(weaponObject);
        weapon = null;
        weaponObject = null;

        if(!createSupplier)
            return true;

        var droppedWeapon = Instantiate(
            WeaponSupplierPrefab, 
            transform.position + Vector3.up * 0.5f + transform.forward * 2, 
            transform.rotation).GetComponent<WeaponSupplier>();

        droppedWeapon.MaxBulletsLoaded = maxBulletsLoaded;
        droppedWeapon.MaxBulletsMagazine = maxBulletsMagazine;
        droppedWeapon.BulletsLoaded = bulletsLoaded;
        droppedWeapon.BulletsMagazine = bulletsMagazine;

        attackSelection = 0;

        return true;
    }

    public bool PickUpWeapon(
        GameObject weaponPrefab, 
        int maxBulletsLoaded, 
        int maxBulletsMagazine, 
        int bulletsLoaded, 
        int bulletsMagazine) 
    {

        if(hasWeapon())
            return false;

        var weaponSpawnPoint = 
        transform.position + 
        transform.forward * transform.localScale.z / 2 + 
        transform.right * transform.localScale.x / 2;

        weaponObject = Instantiate(weaponPrefab, weaponSpawnPoint, transform.rotation, transform);
        weapon = weaponObject.GetComponent<Weapon>();
        weapon.SetState(maxBulletsLoaded, maxBulletsMagazine, bulletsLoaded, bulletsMagazine);
        weapon.MakeBulletCountEnable();
        weapon.UnfocusBulletCount();
        return true;
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log("Particle collision with " + other.name);
    }


    public void TakeDamage(float damage)
    {
        if(_currentHealth <= 0f) {
            //Die();
            return;
        }
        
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

        transform.rotation = Quaternion.Euler(0, -angle + 90, 0);


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

    private bool hasWeapon() {
        return weaponObject != null;
    }

    public void Heal(float hp)
    {
        if(_currentHealth >= _maxHealth)
            return;
        _currentHealth = Math.Min(_currentHealth + hp, _maxHealth);
        _healthBar.SetHealth(_currentHealth);
    }

    public void RegenEnergy(float ep)
    {
        if(_currentEnergy >= _maxEnergy)
            return;

        _currentEnergy = Math.Min(_currentEnergy + ep, _maxEnergy);
        _energyBar.SetEnergy(_currentEnergy);
    }
}
