using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : EnemyController
{
    protected override void Attacking()
    {
        Debug.Log("Vampire is attacking");
    }
}
