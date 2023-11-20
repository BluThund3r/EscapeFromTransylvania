using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountController : MonoBehaviour
{
    private int _bulletsLoaded;
    [SerializeField] private int _maxBulletsLoaded = 30;
    [SerializeField] private int _maxBulletsMagazine = 100;
    private int _bulletsMagazine = 100;
    [SerializeField] private Text _bulletCountText;

    // Start is called before the first frame update
    void Start()
    {
        _bulletsLoaded = _maxBulletsLoaded;
        _bulletsMagazine = _maxBulletsMagazine;
        RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }

    private void RefreshBulletCount(int bulletsLoaded, int bulletsMagazine) {
        _bulletCountText.text = $"{bulletsLoaded}/{bulletsMagazine}";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _bulletsLoaded < _maxBulletsLoaded && _bulletsMagazine > 0){
            var bulletsToReload = Mathf.Min(_bulletsMagazine, _maxBulletsLoaded - _bulletsLoaded);
            _bulletsMagazine -= bulletsToReload;
            _bulletsLoaded += bulletsToReload;
        }

        else if(Input.GetMouseButtonDown(0) && _bulletsLoaded > 0){
            _bulletsLoaded --;
        }

        RefreshBulletCount(_bulletsLoaded, _bulletsMagazine);
    }
}
