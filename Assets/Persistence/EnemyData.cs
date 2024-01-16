using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public EnemyType Type;
    public float Health;
    public Vector3 Position;

    public EnemyData(EnemyController enemy) {
        Health = enemy._health;
        Position = enemy.transform.position;
        Type = enemy.Type;
    }
}
