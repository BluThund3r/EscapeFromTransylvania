using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceGameHandler : MonoBehaviour
{
    public GameObject[] EnemySpawners;
    public GameObject[] GrenadeSuppliers;
    public GameObject[] WeaponSuppliers;
    PersistanceManager persistanceManager;

    private void Awake() {
        persistanceManager = GameObject.Find("PersistanceManager").GetComponent<PersistanceManager>();
        EnemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        GrenadeSuppliers = GameObject.FindGameObjectsWithTag("GrenadeSupplier");
        WeaponSuppliers = GameObject.FindGameObjectsWithTag("WeaponSupplier");
        DeactivateAllObjects();
        if(!persistanceManager.IsGameState()) {
            ActivateAllObjects();
        }
    }

    private void DeactivateAllObjects() {
        foreach(var enemySpawner in EnemySpawners) {
            enemySpawner.SetActive(false);
        }
        foreach(var grenadeSupplier in GrenadeSuppliers) {
            grenadeSupplier.SetActive(false);
        }
        foreach(var weaponSupplier in WeaponSuppliers) {
            weaponSupplier.SetActive(false);
        }
    }

    private void ActivateAllObjects() {
        foreach(var enemySpawner in EnemySpawners) {
            enemySpawner.SetActive(true);
        }
        foreach(var grenadeSupplier in GrenadeSuppliers) {
            grenadeSupplier.SetActive(true);
        }
        foreach(var weaponSupplier in WeaponSuppliers) {
            weaponSupplier.SetActive(true);
        }
    }


}
