using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Harmful : MonoBehaviour
{
    public virtual float Damage() {
        return 10f;
    }
}
