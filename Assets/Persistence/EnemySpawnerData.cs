using UnityEngine;

public class EnemySpawnerData {
    public EnemyType EnemyType;
    public Vector3 Position;

    public EnemySpawnerData(EnemySpawner enemySpawner) {
        EnemyType = enemySpawner.EnemyType;
        Position = enemySpawner.transform.position;
    }
}