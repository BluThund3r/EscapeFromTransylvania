using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 offset;

    public void Awake() {
        _camera = Camera.main;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        _slider.value = currentHealth / maxHealth;
    }

    void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position + offset;
    }
}
