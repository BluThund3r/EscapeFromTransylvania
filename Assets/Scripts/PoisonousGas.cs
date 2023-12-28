using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousGas : Harmful
{
    public override float GetDamage()
    {
        return this.Damage;
    }

    public override void SetDamage(float damage)
    {
        this.Damage = damage;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") 
            other.gameObject.GetComponent<Player>().TakeDamage(GetDamage());
    }
}
