using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuListener : MonoBehaviour
{
    private PersistanceManager persistanceManager;
    public bool IsPaused = false;

    private void Awake() {
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!IsPaused) {  // Pause the game
                IsPaused = true;
                persistanceManager.SaveCurrentGameState();
                Time.timeScale = 0;
                var previousScene = SceneManager.GetActiveScene();
                if(previousScene.GetRootGameObjects()[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
                    audioListener.enabled = false;
                    Debug.Log("Disabled previous camera audio listener");
                }
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            }
            else {
                var cameras = GameObject.FindGameObjectsWithTag("MainCamera");
                foreach(var camera in cameras) {
                    camera.GetComponent<AudioListener>().enabled = false;
                }
                SceneManager.UnloadSceneAsync("PauseMenu");
                var currentScene = SceneManager.GetSceneByName("Game");
                if(currentScene.GetRootGameObjects()[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
                    audioListener.enabled = true;
                }
                Time.timeScale = 1;
                IsPaused = false;
            }
        }
    }

    
}
