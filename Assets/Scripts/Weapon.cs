using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public int bulletsLoaded;
    public int maxBullets;
    private int _bulletsLoaded;
    [SerializeField] private int _maxBulletsLoaded = 5;
    [SerializeField] private int _maxBulletsMagazine = 10;
    private int _bulletsMagazine;
    private static BulletCountController bulletCountController;
    protected static GameObject bulletCountObject;
    private static bool isBulletCountInitialized = false;

    void Awake() {
        _bulletsLoaded = _maxBulletsLoaded;
        _bulletsMagazine = _maxBulletsMagazine;
        bulletSpawnPoint = this.transform;
        if(!isBulletCountInitialized) {
            bulletCountObject = GameObject.Find("BulletCount");
            bulletCountController = bulletCountObject.GetComponent<BulletCountController>();
            isBulletCountInitialized = true;
        }
        this.MakeBulletCountEnable();
        bulletCountController.RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }

    public void Fire() {
        if(_bulletsLoaded > 0) {
            _bulletsLoaded --;
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Bullet>();
            bullet.SetDirection(bulletSpawnPoint.forward);
        }
        bulletCountController.RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }

    public void Reload() {
        if( _bulletsLoaded < _maxBulletsLoaded && _bulletsMagazine > 0) {
            var bulletsToReload = Mathf.Min(_bulletsMagazine, _maxBulletsLoaded - _bulletsLoaded);
            _bulletsMagazine -= bulletsToReload;
            _bulletsLoaded += bulletsToReload;
        }
        bulletCountController.RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }

    public void MakeBulletCountDisable() {
        bulletCountObject.SetActive(false);
    }

    public void MakeBulletCountEnable() {
        bulletCountObject.SetActive(true);
    }
}
