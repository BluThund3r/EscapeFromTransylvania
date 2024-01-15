using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : EnemyController
{
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private float potionThrowAngle = 30f;

    private new void Start() {
        base.Start();
        _timeBetweenAttacks = 5f;
    }

    private Vector3 GetPotionSpawnPoint() {
        return transform.position + transform.forward.normalized;
    }

    protected override void Attacking()
    {
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked) {
            var potion = Instantiate(potionPrefab, GetPotionSpawnPoint(), Quaternion.identity).GetComponent<WitchPotion>();
            potion.Launch(_player.position, potionThrowAngle);


            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks); 
        }
    }
}
