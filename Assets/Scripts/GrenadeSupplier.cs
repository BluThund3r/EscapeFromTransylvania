using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSupplier : MonoBehaviour
{
    public float RotationSpeed = 50f;
    public float BobbingSpeed = 5f;
    public float Amplitude = 0.25f;
    private Coroutine animationCoroutine;
    public float initialY;

    private void Start() {
        initialY = transform.localPosition.y;
        animationCoroutine = StartCoroutine(StartAnimation());
    }

    public void LoadData(GrenadeSupplierData grenadeSupplierData) {
        initialY = grenadeSupplierData.initialY;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && other.GetComponent<Player>()
            .PickUpGrenade()) {
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
