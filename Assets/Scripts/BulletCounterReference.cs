using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCounterReference : MonoBehaviour
{
    public GameObject BulletCounter;

    private void Start() {
        BulletCounter.SetActive(false);
    }
}
