using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Pricolici : EnemyController
{
    public float DealtDamage = 10f;
    private float animationTime = 0.5f;

    [SerializeField] AudioSource attackSound;

    public Transform manutaTransform;
    private new void Start() {
        base.Start();
        _timeBetweenAttacks = 1f;
        Type = EnemyType.Pricolici;
    }

    protected override void Attacking()
    {
        _navMeshAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!_alreadyAttacked)
            StartAttackAnimation();
    }

    private void StartAttackAnimation() {
        _alreadyAttacked = true;
        StartCoroutine(StartAttack());
        attackSound.Play();
    }

    private IEnumerator StartAttack() {
        var currentTime = 0f;
        var initialRotation = manutaTransform.localRotation;
        var destinationRotation = initialRotation * Quaternion.Euler(60f, 0f, 0f);
        var halfTime = animationTime / 2;

        do
        {
            manutaTransform.localRotation = Quaternion.Lerp(initialRotation, destinationRotation, currentTime / halfTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= halfTime);

        _player.gameObject.GetComponent<Player>().TakeDamage(DealtDamage);

        currentTime = 0f;
        do
        {
            manutaTransform.localRotation = Quaternion.Lerp(destinationRotation, initialRotation, currentTime / halfTime);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= halfTime);


        Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        yield break;
    }
}
