
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public List<EnemyData> enemyDataList;
    public TimerData timerData;
    public List<EnemySpawnerData> enemySpawnerDataList;
    public DateTime saveTime;

    public GameData(
        PlayerData playerData, 
        List<EnemyData> enemyDataList, 
        TimerData timerData, 
        List<EnemySpawnerData> enemySpawnerDataList,
        DateTime saveTime
        ) {
        this.playerData = playerData;
        this.enemyDataList = enemyDataList;
        this.timerData = timerData;
        this.enemySpawnerDataList = enemySpawnerDataList;
        this.saveTime = saveTime;
    }
}
