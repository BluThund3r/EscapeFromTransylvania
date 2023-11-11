using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public int bulletsLoaded;
    public int maxBullets;

    void Start() {
        maxBullets = 10;
        bulletsLoaded = 10;
    }

    public void Fire() {
        if(bulletsLoaded > 0) {
            bulletsLoaded -= 1;
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Bullet>();
        }
    }

    public void Reload() {
        bulletsLoaded = maxBullets;
    }
}
