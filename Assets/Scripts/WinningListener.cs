using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningListener : MonoBehaviour
{
    private GameManager gameManager;
    public bool isInEscapeArea = false;
    public Material lockedMaterial;
    public Material unlockedMaterial;
    private MeshRenderer materialRender;

    private bool hasEscaped = false;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        materialRender = GetComponent<MeshRenderer>();
        materialRender.material = lockedMaterial;
    }

    private void Update() {
        if(!AnyEnemiesLeft() && !AnyEnemieSpawnersLeft()) {
                materialRender.material = unlockedMaterial;

            if(isInEscapeArea && !hasEscaped) {
                hasEscaped = true;
                gameManager.LoadScene("WinningScene");
            }
        }
        else {
            materialRender.material = lockedMaterial;
        }
    }

    private bool AnyEnemiesLeft() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length > 0;
    }

    private bool AnyEnemieSpawnersLeft() {
        var enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        return enemySpawners.Length > 0;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            isInEscapeArea = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            isInEscapeArea = false;
        }
    }
}
