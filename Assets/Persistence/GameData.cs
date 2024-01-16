
using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public List<EnemyData> enemyDataList;
    public TimerData timerData;
    public List<EnemySpawnerData> enemySpawnersDataList;
    public List<GrenadeSupplierData> grenadeSuppliersDataList;
    public List<WeaponSupplierData> weaponSuppliersDataList;

    public DateTime saveTime;

    public GameData(
        PlayerData playerData, 
        List<EnemyData> enemyDataList, 
        TimerData timerData, 
        List<EnemySpawnerData> enemySpawnersDataList,
        DateTime saveTime,
        List<GrenadeSupplierData> grenadeSuppliersDataList,
        List<WeaponSupplierData> weaponSuppliersDataList
        ) {
        this.playerData = playerData;
        this.enemyDataList = enemyDataList;
        this.timerData = timerData;
        this.enemySpawnersDataList = enemySpawnersDataList;
        this.saveTime = saveTime;
        this.grenadeSuppliersDataList = grenadeSuppliersDataList;
        this.weaponSuppliersDataList = weaponSuppliersDataList;
    }
}
