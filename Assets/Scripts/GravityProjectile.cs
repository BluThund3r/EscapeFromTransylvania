using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityProjectile : Projectile
{
    private Vector3 direction;
    private bool forceApplied = false;

    void FixedUpdate() 
    {
        if(!_directionSet || forceApplied)
            return;
        gameObject.GetComponent<Rigidbody>().AddForce(direction * projectileSpeed, ForceMode.Impulse);
        forceApplied = true;
    }

    public new void SetDirection(Vector3 direction)
    {
        this.direction = direction;
        _directionSet = true;
    }
}
