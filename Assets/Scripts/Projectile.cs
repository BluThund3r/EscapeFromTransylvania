using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : Harmful
{
    public float life = 3f;
    public float projectileSpeed = 10;
    private bool _directionSet = false;

    void Start()
    {
        Destroy(gameObject, life);
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

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
