using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private readonly Stack<Scene> previousScenes = new(); // To store the previous scene names

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadSceneWithPrevious(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene();
        previousScenes.Push(currentScene); // Store the current scene name
        UnfocusScene(currentScene);
        StartCoroutine(LoadSceneAdditive(sceneName));
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneAdditive(sceneName));
    }

    private void UnfocusScene(Scene scene) {
        var rootGameObjects = scene.GetRootGameObjects();
        if(rootGameObjects[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
            audioListener.enabled = false;
        }
        if(rootGameObjects[rootGameObjects.Length - 1].TryGetComponent<EventSystem>(out EventSystem eventSystem)) {
            eventSystem.enabled = false;
        }
    }

    private void FocusScene(Scene scene) {
        var rootGameObjects = scene.GetRootGameObjects();
        if(rootGameObjects[0].TryGetComponent<AudioListener>(out AudioListener audioListener)) {
            audioListener.enabled = true;
        }
        if(rootGameObjects[rootGameObjects.Length - 1].TryGetComponent<EventSystem>(out EventSystem eventSystem)) {
            eventSystem.enabled = true;
        }
        SceneManager.SetActiveScene(scene);
    }

    public void LoadPreviousScene()
    {
        var previousScene = previousScenes.Pop(); 
        var currentScene = SceneManager.GetActiveScene();
        UnfocusScene(currentScene);
        FocusScene(previousScene);
        SceneManager.UnloadSceneAsync(currentScene);
    }

    private IEnumerator LoadSceneAdditive(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene();
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while(!asyncLoad.isDone) {
            yield return null;
        }

        UnfocusScene(currentScene);
        var newScene = SceneManager.GetSceneByName(sceneName);
        FocusScene(newScene);
        if(newScene.name == "Game" || newScene.name == "MainMenu")
            ClearAllLoadedScenesExceptFor(newScene);
        yield break;
    }

    public void ResumeGame() {
        var currentScene = SceneManager.GetActiveScene();
        UnfocusScene(currentScene);
        FocusScene(SceneManager.GetSceneByName("Game"));
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public void PauseGame() {
        LoadScene("PauseMenu");
    }

    public void ClearAllLoadedScenesExceptFor(Scene scene) {
        for(int i = 0; i < SceneManager.sceneCount; ++ i) {
            var sceneIt = SceneManager.GetSceneAt(i);

            if(scene == sceneIt)
                continue;

            UnfocusScene(sceneIt);
            SceneManager.UnloadSceneAsync(sceneIt);
        }
        previousScenes.Clear();
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAdditive("Game"));
    }

    public void LoadSavedGameScene(string saveName) {
        StartCoroutine(LoadGameSceneAsync(saveName));
    }

    private IEnumerator LoadGameSceneAsync(string saveName) {
        var persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        persistanceManager.LoadStateFromFile(saveName);
        
        var asyncLoad = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
        while(!asyncLoad.isDone) {
            yield return null;
        }

        persistanceManager.LoadCurrentGameStateIntoScene();
        var newScene = SceneManager.GetSceneByName("Game");
        FocusScene(newScene);
        ClearAllLoadedScenesExceptFor(newScene);

        yield break;
    }

    public string GetPreviousSceneName() {
        if(previousScenes.Count > 0)
            return previousScenes.Peek().name;
        else
            return "";
    }

    public void PrintSceneStack() {
        Debug.Log("Scene stack:");
        foreach(var sceneName in previousScenes) {
            Debug.Log(sceneName.name);  
        }
    }
}
