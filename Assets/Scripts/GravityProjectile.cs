using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    protected new void OnCollisionEnter(Collision collision) {
        // empty because no behaviour is needed by default; each subclass should implement its own behaviour
    }
}
