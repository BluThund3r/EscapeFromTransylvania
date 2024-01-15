using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuListener : MonoBehaviour
{
    private PersistanceManager persistanceManager;
    public GameManager gameManager;
    public bool IsPaused = false;

    private void Awake() {
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !IsPaused) { // then pause the game
            IsPaused = true;
            persistanceManager.SaveCurrentGameState();
            Time.timeScale = 0;
            // var previousScene = SceneManager.GetActiveScene();
            // if(previousScene.GetRootGameObjects()[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
            //     audioListener.enabled = false;
            // }
            gameManager.PauseGame();
            // StartCoroutine(LoadPauseMenuAsync());
        }
    }

    IEnumerator LoadPauseMenuAsync() {
        var asyncLoad = SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        while(!asyncLoad.isDone) {
            yield return null;
        }

        var pauseMenuScene = SceneManager.GetSceneByName("PauseMenu");
        SceneManager.SetActiveScene(pauseMenuScene);
        yield break;
    }

    
}
