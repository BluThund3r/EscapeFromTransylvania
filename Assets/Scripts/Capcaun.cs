using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Capcaun : EnemyController
{
    [SerializeField] private GameObject _bulletPrefab;

    private new void Start() {
        base.Start();
        _timeBetweenAttacks = 2f;
    }

    protected override void Attacking()
    {
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked) {
            var projectile = Instantiate(_bulletPrefab, transform.position + transform.forward.normalized, Quaternion.identity).GetComponent<Projectile>();
            projectile.SetDirection(transform.forward);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks); // apeleaza ResetAttack o data la _timeBetweenAttacks secunde
        }
    }
}
