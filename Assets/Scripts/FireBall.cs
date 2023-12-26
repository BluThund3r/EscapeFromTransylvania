using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private PredictTarget target;
    public GameObject DamageSpherePrefab;
    public GameObject TargetPrefab;
    public GameObject ExplosionPrefab;

    void Start()
    {
        target = Instantiate(TargetPrefab, transform.position, Quaternion.identity).GetComponent<PredictTarget>();
        target.SetSphere(transform);
        target.SetScale(2f);
    }

    void OnCollisionEnter(Collision collision) {
        var damageSphere = Instantiate(DamageSpherePrefab, transform.position, Quaternion.identity).GetComponent<DamageSphere>();
        damageSphere.SetRadius(2f);
        damageSphere.SetDamage(20f);
        var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(damageSphere.gameObject, 0.5f);
        target.DestroyTarget();
        Destroy(gameObject);
    }
}
