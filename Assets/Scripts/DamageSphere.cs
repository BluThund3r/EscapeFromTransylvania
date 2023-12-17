using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphere : Harmful
{
    private GameObject damageSphereSummoner;
    void Awake()
    {
        SetDamage(20f);
    }

    public void SetDamageSphereSummoner(GameObject damageSphereSummoner) {
        this.damageSphereSummoner = damageSphereSummoner;
    }

    public override void SetDamage(float damage) {
        this.Damage = damage;
    }

    public override float GetDamage() {
        return this.Damage;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == damageSphereSummoner)
            return;

        if(other.gameObject.CompareTag("Player")) {
            other.GetComponent<Player>().TakeDamage(GetDamage());
        } else if(other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<EnemyController>().TakeDamage(GetDamage());
        }
    }
}
