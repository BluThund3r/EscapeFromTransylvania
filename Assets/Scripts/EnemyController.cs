using System;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _navMeshAgent;
    [SerializeField] protected Transform _player;
    [SerializeField] private LayerMask _groundMask, _playerMask;
    [SerializeField] protected float _health;
    protected float _maxHealth = 100f;
    private Vector3 _walkPoint;
    private bool _isWalkPointSet = false;
    [SerializeField] private float _walkPointRange;

    [SerializeField] protected virtual float _timeBetweenAttacks {get; set;}
    protected bool _alreadyAttacked = false;

    [SerializeField] protected float _sightRange, _attackRange;
    protected bool _playerInSightRange, _playerInAttackRange;
    [SerializeField] private EnemyHealthBar _healthBar;
    public GameObject hpPrefab;
    public GameObject epPrefab;
    private int minHPSpawn = 0, maxHPSpawn = 4, minEPSpawn = 0, maxEPSpawn = 3;

    protected void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _healthBar = GetComponentInChildren<EnemyHealthBar>();
        _playerMask = LayerMask.GetMask("Player");
        _groundMask = LayerMask.GetMask("WhatIsGround");
    }

    protected void Start() {
        _health = _maxHealth;
        _healthBar.UpdateHealthBar(_health, _maxHealth);
    }

    private void Update() {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerMask);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerMask);

        if (_playerInSightRange) {
            if (_playerInAttackRange)
                Attacking();
            else
                Chasing();
        } else {
            Patroling();
        }   
    }

    protected void Patroling() {
        if(!_isWalkPointSet)
            SetWalkPoint();
        
        if(_isWalkPointSet) {
            _navMeshAgent.SetDestination(_walkPoint);
        }
            

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
        _navMeshAgent.SetDestination(_player.position);
        transform.LookAt(_player);
    }

    protected abstract void Attacking(); 

    protected void ResetAttack() {
        _alreadyAttacked = false;
    }

    public void TakeDamage(float damage) {
        _health -= damage;
        _healthBar.UpdateHealthBar(_health, _maxHealth);

        if(_health <= 0) 
            Die();
    }

    protected void Die() {
        SpanwnDrops();
        Destroy(gameObject);
    }

    private void SpanwnDrops()
    {
        var noHPSpawn = Random.Range(minHPSpawn, maxHPSpawn);
        for (int i = 0; i < noHPSpawn; i++)
        {
            var randomSmallZ = Random.Range(-0.5f, 0.5f);
            var randomSmallX = Random.Range(-0.5f, 0.5f);
            Instantiate(hpPrefab, transform.position + new Vector3(randomSmallX, 0.5f, randomSmallZ), Quaternion.identity);
        }

        var noEPSpawn = Random.Range(minEPSpawn, maxEPSpawn);
        for (int i = 0; i < noEPSpawn; i++)
        {
            var randomSmallZ = Random.Range(-0.5f, 0.5f);
            var randomSmallX = Random.Range(-0.5f, 0.5f);
            Instantiate(epPrefab, transform.position + new Vector3(randomSmallX, 0.5f, randomSmallZ), Quaternion.identity);
        }

        Debug.Log($"Spawned {noEPSpawn} EP,  {noHPSpawn} HP");
    }
}
