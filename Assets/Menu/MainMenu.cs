using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LoadingImage;
    private GameManager gameManager;
    private PersistanceManager persistanceManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
    }

    private void Start() {
        LoadingImage.SetActive(false);
    }

    private void hideButtons() {
        GameObject.Find("MenuButtons").SetActive(false);
    }

    private void showLoading() {
        LoadingImage.SetActive(true);
    }

    public void NewGame() {
        hideButtons();
        showLoading();
        gameManager.LoadSceneAsync("Game");
    }

    public void LoadGame() {
        Debug.Log("Load menu");
        // gameManager.LoadSceneWithPrevious("LoadMenu");
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    public void Options() {
        gameManager.LoadSceneWithPrevious("OptionsMenu");
    }
}
