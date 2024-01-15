using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchPotion : GravityProjectile
{  
    public GameObject poisonAreaPrefab;
    public float potionThrowAngle = 30f;

    private void Start() {
        projectileSpeed = 8f;
        SetDamage(0.15f);
    }

    private void FixedUpdate() {}

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

    public void Launch(Vector3 target, float potionThrowAngle) {
        var rb = gameObject.GetComponent<Rigidbody>();
        var velocity = CalcVelocity(transform.position, target, potionThrowAngle);
        rb.AddForce(velocity, ForceMode.Impulse);
    }

    private Vector3 CalcVelocity(Vector3 source, Vector3 target, float angle) {
        Vector3 direction = target - source;            				
		float h = direction.y;                                        
		direction.y = 0;                                               
		float distance = direction.magnitude;                        
		float a = angle * Mathf.Deg2Rad;                                
		direction.y = distance * Mathf.Tan(a);                            
		distance += h/Mathf.Tan(a);
        distance = Mathf.Abs(distance);                                      

		// calculate velocity
		float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2*a));
        var finalVelocity = velocity * direction.normalized;
		return finalVelocity;
    }
}
