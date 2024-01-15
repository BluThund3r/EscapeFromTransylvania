using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PersistanceManager persistanceManager;
    private GameManager gameManager;

    public void Awake() {
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.PrintSceneStack();
    }

    public void ResumeGame() {
        Debug.Log("Resuming game...");
        // var cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        // foreach(var camera in cameras) {
        //     camera.GetComponent<AudioListener>().enabled = false;
        // }
        // SceneManager.UnloadSceneAsync("PauseMenu");
        // var currentScene = SceneManager.GetSceneByName("Game");
        // if(currentScene.GetRootGameObjects()[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
        //     audioListener.enabled = true;
        // }
        // Time.timeScale = 1;
        // SceneManager.SetActiveScene(currentScene);
        gameManager.ResumeGame();

        Time.timeScale = 1;
        GameObject.Find("PauseMenuHandler").GetComponent<PauseMenuListener>().IsPaused = false;
    }

    public void SaveGame() {
        Debug.Log("Saving game...");
        if(persistanceManager.IsAnySaveLoaded())
            persistanceManager.SaveCurrentStateToExistentFile();
        else
            gameManager.LoadSceneWithPrevious("GameSavingMenu");
    }

    public void Options() {
        Debug.Log("Options");
        gameManager.LoadSceneWithPrevious("OptionsMenu");
    }

    public void BackToMainMenu() {
        Debug.Log("Back to main menu");
        persistanceManager.ClearCurrentSave();
        gameManager.LoadScene("MainMenu");
        gameManager.ClearAllLoadedScenesExceptFor(SceneManager.GetSceneByName("MainMenu"));
    }
}
