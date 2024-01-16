using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int _bulletsLoaded;
    public int _maxBulletsLoaded;
    public int _maxBulletsMagazine;
    public int _bulletsMagazine;
    private BulletCountController bulletCountController;
    protected GameObject bulletCountObject;
    public Vector3 realScale;
    [SerializeField] AudioSource fireSound;

    private Vector3 bulletSpawnPoint() {
        return transform.position + transform.forward;
    }

    public void SetState(int maxBulletsLoaded, int maxBulletsMagazine, int bulletsLoaded, int bulletsMagazine) {
        _maxBulletsLoaded = maxBulletsLoaded;
        _maxBulletsMagazine = maxBulletsMagazine;
        _bulletsLoaded = bulletsLoaded;
        _bulletsMagazine = bulletsMagazine;
        bulletCountController.RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }

    void Awake() {
        bulletCountObject = GameObject.Find("BulletCounterReference").GetComponent<BulletCounterReference>().BulletCounter;
        bulletCountController = bulletCountObject.GetComponent<BulletCountController>();
        
        this.MakeBulletCountEnable();
        bulletCountController.RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }

    void Update()
    {
        // Adjust the child's scale to compensate for the parent's scale
        transform.localScale = new Vector3(
            realScale.x / transform.parent.localScale.x,
            realScale.y / transform.parent.localScale.y,
            realScale.z / transform.parent.localScale.z
        );
    }

    public void Fire() {
        if(_bulletsLoaded > 0) {
            _bulletsLoaded --;
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint(), transform.rotation).GetComponent<Bullet>();
            bullet.SetDirection(transform.parent.forward);
            fireSound.Play();
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

    public void FocusBulletCount() {
        bulletCountController.Focus();
    }

    public void UnfocusBulletCount() {
        bulletCountController.Unfocus();
    }
}
