using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class PersistanceManager : MonoBehaviour
{
    private string savesDirectoryPath;
    private GameData currentGameState;
    public List<GameObject> EnemyPrefabs;
    public List<GameObject> EnemySpawnerPrefab;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        savesDirectoryPath = Path.Combine(Application.persistentDataPath, "saves");
        CreateSavesPath(); 
    }

    private void CreateSavesPath() {
        if(!Directory.Exists(savesDirectoryPath)) {
            Directory.CreateDirectory(savesDirectoryPath);
        }
    }

    public string GetSavesPath() {
        return savesDirectoryPath;
    }

    public void UpdateCurrentGameState(GameData gameData) {
        currentGameState = gameData;
    }

    public void SaveCurrentGameState() {
        var playerData = new PlayerData(GameObject.Find("Player").GetComponent<Player>());
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var enemyDataList = new List<EnemyData>();
        foreach(var enemy in enemies) {
            enemyDataList.Add(new EnemyData(enemy.GetComponent<EnemyController>()));
        }
        var timerData = new TimerData(GameObject.Find("Timer").GetComponent<Timer>());
        var enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        var enemySpawnerDataList = new List<EnemySpawnerData>();
        foreach(var enemySpawner in enemySpawners) {
            enemySpawnerDataList.Add(
                new EnemySpawnerData(enemySpawner.GetComponent<EnemySpawner>())
            );
        }
        currentGameState = new GameData(playerData, enemyDataList, timerData, enemySpawnerDataList);
    }

    public void LoadCurrentGameState() {
        // Loading player 
        var player = GameObject.Find("Player").GetComponent<Player>();
        player.LoadData(currentGameState.playerData);

        // Loading enemies
        foreach(var enemyData in currentGameState.enemyDataList) {
            var enemy = Instantiate(EnemyPrefabs[(int)enemyData.Type], enemyData.Position, Quaternion.identity)
                            .GetComponent<EnemyController>();
            enemy.SetHealth(enemyData.Health);
        }

        // Loading timer
        var timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer.LoadData(currentGameState.timerData);

        // Loading enemy spawners
        foreach(var enemySpawnerData in currentGameState.enemySpawnerDataList) {
            Instantiate(
                EnemySpawnerPrefab[(int)enemySpawnerData.EnemyType], 
                enemySpawnerData.Position, Quaternion.identity
            );
        }
    }

    public GameData GetCurrentGameState() {
        return currentGameState;
    }

    public void SaveCurrentStateToFile(string saveName) {
        var savePath = Path.Combine(savesDirectoryPath, saveName + ".json");
        var json = JsonUtility.ToJson(currentGameState);
        File.WriteAllText(savePath, json);
    }

    public void LoadStateFromFile(string saveName) {
        var savePath = Path.Combine(savesDirectoryPath, saveName + ".json");
        var json = File.ReadAllText(savePath);
        currentGameState = JsonUtility.FromJson<GameData>(json);
    }

    public void DeleteSaveFile(string saveName) {
        var savePath = Path.Combine(savesDirectoryPath, saveName + ".json");
        File.Delete(savePath);
    }

    public string[] GetSaveNames() {
        return Directory.GetFiles(savesDirectoryPath, "*.json");
    }
}
