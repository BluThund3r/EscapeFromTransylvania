using System;
using UnityEngine;

[Serializable]
public class GrenadeSupplierData {
    public float initialY;
    public Vector3 position;

    public GrenadeSupplierData(GrenadeSupplier grenadeSupplier) {
        initialY = grenadeSupplier.initialY;
        position = grenadeSupplier.transform.position;
    }
}