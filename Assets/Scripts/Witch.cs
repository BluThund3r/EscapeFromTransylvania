using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : EnemyController
{
    [SerializeField] private GameObject potionPrefab;

    private new void Start() {
        base.Start();
        _timeBetweenAttacks = 5f;
    }

    protected override void Attacking()
    {
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked) {
            var potion = Instantiate(potionPrefab, transform.position + transform.forward.normalized, Quaternion.identity).GetComponent<WitchPotion>();
            potion.SetDirection(transform.forward + Vector3.up * 0.5f);

            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks); 
        }
    }
}
