using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSupplier : MonoBehaviour
{
    public int MaxBulletsMagazine;
    public int MaxBulletsLoaded;
    public int BulletsLoaded;
    public int BulletsMagazine;
    public float RotationSpeed = 100f;
    public float BobbingSpeed = 1f;
    public float Amplitude = 0.25f;
    private Coroutine animationCoroutine;
    private float initialY;

    private void Start() {
        initialY = transform.localPosition.y;
        animationCoroutine = StartCoroutine(StartAnimation());
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && other.GetComponent<Player>()
            .PickUpWeapon(MaxBulletsLoaded, MaxBulletsMagazine, BulletsLoaded, BulletsMagazine)) {
            StopCoroutine(animationCoroutine);
            Destroy(gameObject);
        }
    }

    private IEnumerator StartAnimation() {
        while(true) {
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            var newY = initialY + Mathf.Sin(Time.time * BobbingSpeed) * Amplitude;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
            yield return null;
        }
    }
}
