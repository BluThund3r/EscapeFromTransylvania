
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public List<EnemyData> enemyDataList;
    public TimerData timerData;
    public List<EnemySpawnerData> enemySpawnerDataList;

    public GameData(
        PlayerData playerData, 
        List<EnemyData> enemyDataList, 
        TimerData timerData, 
        List<EnemySpawnerData> enemySpawnerDataList
        ) {
        this.playerData = playerData;
        this.enemyDataList = enemyDataList;
        this.timerData = timerData;
        this.enemySpawnerDataList = enemySpawnerDataList;
    }
}
