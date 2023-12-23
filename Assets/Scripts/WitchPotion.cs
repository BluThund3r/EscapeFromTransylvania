using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchPotion : GravityProjectile
{  
    public GameObject poisonAreaPrefab;
    private void Start() {
        projectileSpeed = 8f;
        SetDamage(0.1f);
    }

    protected new void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.layer == LayerMask.NameToLayer("WhatIsGround") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Player") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SummonPoisonArea();
            Destroy(gameObject);
        }
    }

    private void SummonPoisonArea() {
        GameObject poisonArea = Instantiate(poisonAreaPrefab, transform.position, Quaternion.identity);
        poisonArea.GetComponent<PoisonArea>().SetDamage(GetDamage());
    }
}
