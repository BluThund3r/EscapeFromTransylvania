using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solomonar : EnemyController
{
    [SerializeField] private GameObject fireballPrefab;

    private new void Start() {
        base.Start();
        _timeBetweenAttacks = 7f;
    }

    protected override void Attacking()
    {
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked) {
            var fireball = Instantiate(fireballPrefab, _player.position + Vector3.up * 40, Quaternion.identity);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
}
