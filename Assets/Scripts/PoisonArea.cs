using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonArea : DamageSphere
{
    public float life = 7f;
    private void Start() {
        Destroy(gameObject, life);
    }
    
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            other.GetComponent<Player>().TakeDamage(GetDamage());
        } else if(other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().TakeDamage(GetDamage());
        }
    }
}
