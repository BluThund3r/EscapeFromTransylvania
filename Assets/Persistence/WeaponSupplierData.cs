using System;
using UnityEngine;

[Serializable]
public class WeaponSupplierData {
    public Vector3 position;
    public float initialY;
    public int maxBulletsMagazine;
    public int maxBulletsLoaded;
    public int bulletsLoaded;
    public int bulletsMagazine;

    public WeaponSupplierData(
        Vector3 position,
        float initialY,
        int maxBulletsMagazine,
        int maxBulletsLoaded,
        int bulletsLoaded,
        int bulletsMagazine
    ) {
        this.position = position;
        this.initialY = initialY;
        this.maxBulletsMagazine = maxBulletsMagazine;
        this.maxBulletsLoaded = maxBulletsLoaded;
        this.bulletsLoaded = bulletsLoaded;
        this.bulletsMagazine = bulletsMagazine;
    }
}