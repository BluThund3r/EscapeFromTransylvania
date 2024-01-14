using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private Stack<string> previousSceneNames = new Stack<string>(); // To store the previous scene names

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Make the GameManager persistent
    }

    public void LoadSceneWithPrevious(string sceneName)
    {
        previousSceneNames.Push(SceneManager.GetActiveScene().name); // Store the current scene name
        LoadScene(sceneName);
    }

    public void LoadSceneWithPreviousAsync(string sceneName)
    {
        previousSceneNames.Push(SceneManager.GetActiveScene().name); // Store the current scene name
        LoadSceneAsync(sceneName);
    }

    public void LoadPreviousScene()
    {
        var previousScene = previousSceneNames.Pop(); // Get the previous scene name
        LoadScene(previousScene); // Load the previous scene
    }

    public void LoadPreviousSceneAsync()
    {
        var previousScene = previousSceneNames.Pop(); // Get the previous scene name
        LoadSceneAsync(previousScene); // Load the previous scene
    }

    public void ClearPreviousScenes()
    {
        previousSceneNames.Clear(); // Clear the previous scene names
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }

    public void LoadScene(int sceneIndex)
    {
        var sceneName = SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName) 
    {
        var persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        if(sceneName != "Game" || !persistanceManager.IsGameState())
            SceneManager.LoadSceneAsync(sceneName);
        else
            LoadAndUpdateGameScene();
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        var sceneName = SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        LoadSceneAsync(sceneName);
    }

    public void LoadAndUpdateGameScene(string saveName = "") {
        StartCoroutine(LoadGameSceneAsync(saveName));
    }

    private IEnumerator LoadGameSceneAsync(string saveName) {
        var previousScene = SceneManager.GetActiveScene();
        var persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        if(saveName != "")
            persistanceManager.LoadStateFromFile(saveName);

        if(previousScene.GetRootGameObjects()[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
            audioListener.enabled = false;
            Debug.Log("Disabled previous camera audio listener");
        }
        
        var asyncLoad = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        while(!asyncLoad.isDone) {
            yield return null;
        }

        persistanceManager.LoadCurrentGameStateIntoScene();
        var unloadAction = SceneManager.UnloadSceneAsync(previousScene);

        while(!unloadAction.isDone) {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));

        yield break;
    }
}
