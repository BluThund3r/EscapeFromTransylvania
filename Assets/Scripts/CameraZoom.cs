using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5.0f;
    public static float minZoom = 10.0f;
    public static float maxZoom = 100.0f;
    private Camera myCamera;

    private void Awake() {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if(Time.timeScale == 0)
            return;
            
        myCamera.fieldOfView = PlayerPrefs.GetFloat("FOV", minZoom + maxZoom / 2);
        // Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            var tempFOV = myCamera.fieldOfView - zoomSpeed;   
            myCamera.fieldOfView = Mathf.Clamp(tempFOV, minZoom, maxZoom);
            PlayerPrefs.SetFloat("FOV", myCamera.fieldOfView);
        }

        // Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            var tempFOV = myCamera.fieldOfView + zoomSpeed;
            myCamera.fieldOfView = Mathf.Clamp(tempFOV, minZoom, maxZoom);
            PlayerPrefs.SetFloat("FOV", myCamera.fieldOfView);
        }
    }

    private void OnEnable() {
        myCamera.fieldOfView = PlayerPrefs.GetFloat("FOV", minZoom + maxZoom / 2);
    }
}
