using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningAndDeathScreen : MonoBehaviour
{
    private PersistanceManager persistanceManager;  
    private GameManager gameManager;

    private void Awake() {
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void BackToMainMenuWinning() {
        if(persistanceManager.IsAnySaveLoaded()) 
            persistanceManager.DeleteLoadedSave();
        
        gameManager.LoadScene("MainMenu");
        gameManager.ClearAllLoadedScenesExceptFor(SceneManager.GetSceneByName("MainMenu"));
    }

    public void BackToMainMenuDeath() {
        gameManager.LoadScene("MainMenu");
        gameManager.ClearAllLoadedScenesExceptFor(SceneManager.GetSceneByName("MainMenu"));
    }
}
