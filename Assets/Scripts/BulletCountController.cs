using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountController : MonoBehaviour
{
    
    [SerializeField] private Text _bulletCountText;


    public void RefreshBulletCount(int bulletsLoaded, int bulletsMagazine) {
        _bulletCountText.text = $"{bulletsLoaded}/{bulletsMagazine}";
    }

}
