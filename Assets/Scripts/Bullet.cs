using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    void Awake()
    {
        SetDamage(20f);
    }

}
