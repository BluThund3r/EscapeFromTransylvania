using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuListener : MonoBehaviour
{
    private GameManager gameManager;

    private PersistanceManager persistanceManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            //! TODO: make the game freeze when the pause menu is open
            gameManager.LoadSceneWithPrevious("PauseScreen");
        }
    }

    
}
