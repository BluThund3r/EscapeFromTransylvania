using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : GravityProjectile
{
    public float ExplosionRadius = 5f;
    public float ExplosionDamage = 50f;
    public float ExplosionDelay = 3f;
    public GameObject ExplosionPrefab;
    public GameObject DamageSpherePrefab;

    private void Awake() {
        StartCoroutine(Explode());
    }

    private new void Start() {}

    private IEnumerator Explode() {
        yield return new WaitForSeconds(ExplosionDelay);
        var damageSphere = Instantiate(DamageSpherePrefab, transform.position, Quaternion.identity).GetComponent<DamageSphere>();
        damageSphere.SetRadius(ExplosionRadius);
        damageSphere.SetDamage(ExplosionDamage);
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(damageSphere.gameObject, 0.5f);
        Destroy(gameObject);
    }
}
