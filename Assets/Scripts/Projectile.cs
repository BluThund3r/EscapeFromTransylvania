using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : Harmful
{
    public float life = 3f;
    public float projectileSpeed = 10;
    protected bool _directionSet = false;

    private bool alreadyCollided = false;

    void Awake()
    {
        SetDamage(10f);
    }

    void Start()
    {
        Destroy(gameObject, life);
    }

    public override void SetDamage(float damage) {
        this.Damage = damage;
    }

    public override float GetDamage() {
        return this.Damage;
    }

    void FixedUpdate() 
    {
        if(!_directionSet)
            return;
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * projectileSpeed;
    }

    public void SetDirection(Vector3 direction)
    {
        gameObject.transform.forward = direction;
        _directionSet = true;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if(alreadyCollided)
            return;

        // Debug.Log("colision with " + collision.gameObject.name + " detected");
        if(collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().TakeDamage(GetDamage());
            alreadyCollided = true;
        } else if(collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(GetDamage());
            alreadyCollided = true;
        }
        
        Destroy(gameObject);
    }
}
