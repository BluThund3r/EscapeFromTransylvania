using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSupplier : MonoBehaviour
{
    public GameObject WeaponPrefab;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && other.GetComponent<Player>().PickUpWeapon(WeaponPrefab))
            Destroy(gameObject);
    }
}
