using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTimer : MonoBehaviour
{
    private GameObject _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = GameObject.Find("InnerTimer");
        _timer.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            _timer.SetActive(!_timer.activeSelf);
        }
    }
}
