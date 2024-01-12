using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    void Awake()
    {
        SetDamage(20f);
    }

    protected new void OnCollisionEnter(Collision collision) {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        base.OnCollisionEnter(collision);
    }

}
