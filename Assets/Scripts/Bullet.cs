using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float life = 3f;
    public float bulletSpeed = 10;

    void Start()
    {
        Destroy(gameObject, life);
    }

    void FixedUpdate() 
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * bulletSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
