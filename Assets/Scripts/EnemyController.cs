using System.Net;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected Transform _player;
    [SerializeField] private LayerMask _groundMask, _playerMask;
    [SerializeField] protected float _health;
    [SerializeField] protected float _maxHealth = 100f;
    private Vector3 _walkPoint;
    private bool _isWalkPointSet = false;
    [SerializeField] private float _walkPointRange;

    [SerializeField] protected float _timeBetweenAttacks;
    protected bool _alreadyAttacked = false;

    [SerializeField] protected float _sightRange, _attackRange;
    protected bool _playerInSightRange, _playerInAttackRange;
    [SerializeField] private EnemyHealthBar _healthBar;


    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthBar = GetComponentInChildren<EnemyHealthBar>();
        _playerMask = LayerMask.GetMask("Player");
        _groundMask = LayerMask.GetMask("WhatIsGround");
    }

    private void Start() {
        _health = _maxHealth;
        _healthBar.UpdateHealthBar(_health, _maxHealth);
    }

    private void Update() {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerMask);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerMask);

        if(!_playerInSightRange && !_playerInAttackRange) 
            Patroling();
        else if(_playerInSightRange && !_playerInAttackRange)
            Chasing();
        else if(_playerInSightRange && _playerInAttackRange)
            Attacking();
    }

    protected void Patroling() {
        Debug.LogError("Patroling");
        if(!_isWalkPointSet)
            SetWalkPoint();
        
        if(_isWalkPointSet)
            _navMeshAgent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;
        if(distanceToWalkPoint.magnitude < 0.1f)
            _isWalkPointSet = false;
    }

    protected void SetWalkPoint() {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); 

        if(Physics.Raycast(_walkPoint, -transform.up, 2.0f, _groundMask))
            _isWalkPointSet = true;
    }

    protected void Chasing() {
        Debug.LogError("Chasing");
        _navMeshAgent.SetDestination(_player.position);
        transform.LookAt(_player);
    }

    protected abstract void Attacking(); 

    protected void ResetAttack() {
        _alreadyAttacked = false;
    }

    protected void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Harmful")) {
            float damage = other.gameObject.GetComponent<Harmful>().Damage();
            TakeDamage(damage);
        }
    }

    protected void TakeDamage(float damage) {
        _health -= damage;
        _healthBar.UpdateHealthBar(_health, _maxHealth);

        if(_health <= 0) 
            Die();
    }

    protected void Die() {
        Destroy(gameObject);
    }
}
