using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Stack<string> previousSceneNames = new Stack<string>(); // To store the previous scene name

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Make the GameManager persistent
    }

    public void LoadSceneWithPrevious(string sceneName)
    {
        previousSceneNames.Push(SceneManager.GetActiveScene().name); // Store the current scene name
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneWithPreviousAsync(string sceneName)
    {
        previousSceneNames.Push(SceneManager.GetActiveScene().name); // Store the current scene name
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadPreviousScene()
    {
        var previousScene = previousSceneNames.Pop(); // Get the previous scene name
        SceneManager.LoadScene(previousScene); // Load the previous scene
    }

    public void LoadPreviousSceneAsync()
    {
        var previousScene = previousSceneNames.Pop(); // Get the previous scene name
        SceneManager.LoadSceneAsync(previousScene); // Load the previous scene
    }

    public void ClearPreviousScenes()
    {
        previousSceneNames.Clear(); // Clear the previous scene names
    }
}
