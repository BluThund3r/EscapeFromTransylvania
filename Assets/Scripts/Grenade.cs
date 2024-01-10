using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : GravityProjectile
{
    public float ExplosionRadius = 2.5f;
    public float ExplosionDamage = 50f;
    public float ExplosionDelay = 3f;
    public float ParticleStartSize = 4f;
    public GameObject ExplosionPrefab;
    public GameObject DamageSpherePrefab;
    public Rigidbody grenadeRb;

    public float collisionDrag = 5f;

    private void Awake() {
        StartCoroutine(Explode());
    }

    private new void Start() {}

    private IEnumerator Explode() {
        yield return new WaitForSeconds(ExplosionDelay);
        var damageSphere = Instantiate(DamageSpherePrefab, transform.position, Quaternion.identity).GetComponent<DamageSphere>();
        damageSphere.SetRadius(ExplosionRadius);
        damageSphere.SetDamage(ExplosionDamage);
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity).GetComponent<Explosion>();
        explosion.SetParticleStartSize(ParticleStartSize);
        Destroy(explosion.gameObject, 2f);
        Destroy(damageSphere.gameObject, 0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        grenadeRb.drag = collisionDrag;
        grenadeRb.angularDrag = collisionDrag;
    }
}
