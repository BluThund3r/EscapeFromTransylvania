using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
     [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    
    public void SetMaxEnergy(float energy) {
        _slider.maxValue = energy;
        _slider.value = energy;
        _fill.color = _gradient.Evaluate(1f);
    }
    
    public void SetEnergy(float energy) {
        _slider.value = energy;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}
