using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _groundMask, _playerMask;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 25f;
    [SerializeField] private float _health = 100f;
    private Vector3 _walkPoint;
    private bool _isWalkPointSet = false;
    [SerializeField] private float _walkPointRange;

    [SerializeField] private float _timeBetweenAttacks;
    private bool _alreadyAttacked = false;

    [SerializeField] private float _sightRange, _attackRange;
    private bool _playerInSightRange, _playerInAttackRange;


    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
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

    private void Patroling() {
        Debug.Log("Patroling");
        if(!_isWalkPointSet)
            SetWalkPoint();
        
        if(_isWalkPointSet)
            _navMeshAgent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;
        if(distanceToWalkPoint.magnitude < 0.1f)
            _isWalkPointSet = false;
    }

    private void SetWalkPoint() {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = Random.Range(-_walkPointRange, _walkPointRange);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); 

        if(Physics.Raycast(_walkPoint, -transform.up, 2.0f, _groundMask))
            _isWalkPointSet = true;
    }

    private void Chasing() {
        Debug.Log("Chasing");
        _navMeshAgent.SetDestination(_player.position);
        transform.LookAt(_player);
    }

    private void Attacking() {
        // Debug.Log("Attacking " + _alreadyAttacked);
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked) {
            var bulletRb = Instantiate(_bulletPrefab, transform.position + transform.forward.normalized, Quaternion.identity).GetComponent<Rigidbody>();
            bulletRb.useGravity = false;
            bulletRb.AddForce(transform.forward.normalized * _bulletSpeed, ForceMode.Impulse);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks); // apeleaza ResetAttack o data la _timeBetweenAttacks secunde
        }
    }

    private void ResetAttack() {
        _alreadyAttacked = false;
    }

    private void TakeDamage(float damage) {
        _health -= damage;

        if(_health <= 0) 
            Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy() {
        Destroy(this.gameObject);
    }
}
