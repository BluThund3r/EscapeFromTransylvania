using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public float RotationSpeed = 100f;
    
    void Update()
    {
        transform.Rotate(0, 0, RotationSpeed);
    }
}
