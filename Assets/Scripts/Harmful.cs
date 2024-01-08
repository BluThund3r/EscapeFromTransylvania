using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Harmful : MonoBehaviour
{
    protected float Damage = 20f;

    abstract public void SetDamage(float damage);

    abstract public float GetDamage();
}
