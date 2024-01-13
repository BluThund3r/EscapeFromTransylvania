using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Back() {
        gameManager.LoadPreviousScene();
    }
}
