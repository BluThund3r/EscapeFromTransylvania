using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }
    
    public void SetParticleStartSize(float size) {
        var main = ps.main;
        main.startSize = size;
    }
}
