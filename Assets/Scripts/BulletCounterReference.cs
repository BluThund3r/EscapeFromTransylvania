using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCounterReference : MonoBehaviour
{
    public GameObject bulletCounter;

    void Start() {
        bulletCounter.SetActive(false);
    }
}
