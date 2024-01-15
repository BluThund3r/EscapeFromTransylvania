using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake() {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = PlayerPrefs.GetFloat("AudioVolume", 0.5f);
        volumeSlider.onValueChanged.AddListener(OnSliderChange);
    }

    private void OnSliderChange(float value) {
        PlayerPrefs.SetFloat("AudioVolume", value);
        AudioListener.volume = value;
    }
}
