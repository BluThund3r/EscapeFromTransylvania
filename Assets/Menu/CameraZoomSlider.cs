using UnityEngine;
using UnityEngine.UI;

public class CameraZoomSlider : MonoBehaviour
{
    private float minZoom = CameraZoom.minZoom;
    private float maxZoom = CameraZoom.maxZoom;
    private Slider zoomSlider;

    private void Awake() {
        zoomSlider = GetComponent<Slider>();
        zoomSlider.value = zoomToValue(PlayerPrefs.GetFloat("FOV", minZoom + maxZoom / 2));
        zoomSlider.onValueChanged.AddListener(OnSliderChange);
    }

    private void OnSliderChange(float value) {
        PlayerPrefs.SetFloat("FOV", valueToZoom(value));
    }

    private float zoomToValue(float zoom) {
        return (zoom - minZoom) / (maxZoom - minZoom);
    }

    private float valueToZoom(float value) {
        return value * (maxZoom - minZoom) + minZoom;
    }
}
