using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCounterReference : MonoBehaviour
{
    public GameObject GrenadeCounter;

    private void Start() {
        GrenadeCounter.SetActive(false);
    }
}
