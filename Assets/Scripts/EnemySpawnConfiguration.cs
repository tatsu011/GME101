using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnConfiguration", menuName = "Scriptable Objects/EnemySpawnConfiguration")]
public class EnemySpawnConfiguration : ScriptableObject
{
    public Enemy enemyPrefab;
    public Vector2[] spawnOffsets;
    public int spawnCount
    {
        get { return spawnOffsets.Length; }
    }
    public int spawnerID;
    public float delay;
    public PostWaveFlag flag = 0;
}

public enum PostWaveFlag
{
    None = 0,
    UntilAllClear,
    BossWave
}
