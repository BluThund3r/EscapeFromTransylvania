using System;
using UnityEngine;

[Serializable]
public class EnemySpawnerData {
    public EnemyType EnemyType;
    public Vector3 Position;
    public int SpawnCount;
    public EnemySpawnerData(EnemySpawner enemySpawner) {
        EnemyType = enemySpawner.EnemyType;
        Position = enemySpawner.transform.position;
        SpawnCount = enemySpawner._spawnCount;
    }
}