using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : EnemyController
{
    private float maxBounce = 0.25f;
    private float animationTime = 2f;
    private int animationCount = 3;
    private Vector3 originalPosition;
    private float dashTime = 0.25f;
    [SerializeField] private GameObject damageSpherePrefab;

    private DamageSphere damageSphere;

    private new void Start() {
        base.Start();
        _timeBetweenAttacks = 3f;
        Type = EnemyType.Vampire;
    }

    protected override void Attacking()
    {
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked)
            StartDashing();
    }

    private void StartDashing() {
        _alreadyAttacked = true;
        StartCoroutine(StartChargeAnimation());
    }

    private IEnumerator StartChargeAnimation() {
        originalPosition = transform.localPosition; // Store the original scale
        Vector3 destinationPosition = transform.localPosition + maxBounce * new Vector3(0, 1, 0);
        float bounceTime = animationTime / (2*animationCount);
        float currentTime;
        for(int i = 0; i < animationCount; ++ i) {
            currentTime = 0f;
            do
            {
                transform.localPosition = Vector3.Lerp(originalPosition, destinationPosition, currentTime / bounceTime);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= bounceTime);

            currentTime = 0f;
            do
            {
                transform.localPosition = Vector3.Lerp(destinationPosition, originalPosition, currentTime / bounceTime);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= bounceTime);
        }
        
        StartCoroutine(Dash());

        yield break;
    }

    private IEnumerator Dash() {
        transform.localPosition = originalPosition;
        transform.LookAt(_player);
        float currentTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 destinationPosition = _player.position + (transform.position - _player.position).normalized;
        do
        {
            transform.position = Vector3.Lerp(initialPosition, destinationPosition, currentTime / dashTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= dashTime);

        SpawnDamageSphere();

        yield break;
    }

    public void SpawnDamageSphere() {
        damageSphere = Instantiate(damageSpherePrefab, transform.position, Quaternion.identity).GetComponent<DamageSphere>();
        damageSphere.SetDamageSphereSummoner(gameObject);
        Destroy(damageSphere.gameObject, 0.5f);

        Invoke(nameof(ResetAttack), _timeBetweenAttacks);
    }
}
