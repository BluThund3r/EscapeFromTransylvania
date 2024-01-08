using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchPotion : GravityProjectile
{  
    public GameObject poisonAreaPrefab;
    private void Start() {
        projectileSpeed = 8f;
        SetDamage(0.15f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.layer == LayerMask.NameToLayer("WhatIsGround"))
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
