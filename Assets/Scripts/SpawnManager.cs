using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.STP;

public class SpawnManager : MonoBehaviour
{

    [Header("Enemy Spawn Settings")]
    [SerializeField]
    EnemySpawnConfiguration[] _enemiesToSpawn; 
    [SerializeField]
    Transform[] _spawners;
    [SerializeField]
    Direction[] _spawnerFacingDirections;
    [SerializeField]
    Transform _enemyContainer;

    [Header("Powerup Spawn Settings")]
    [Tooltip("sets a rate in seconds to spawn powerups.  min: x, max: y")]
    [SerializeField]
    Vector2 _powerUpSpawnRate = new Vector2(3f, 7f);
    [SerializeField]
    GameObject[] _powerUpOptions;
    [SerializeField]
    Transform _powerUpContainer;
    [SerializeField]
    float _powerUpSpawnDelay;

    [Header("General Spawn Settings")]
    [Tooltip("sets a range.  min: x, max: y")]
    [SerializeField]
    Vector2 _xRange = new Vector2(-8f, 8f);
    [SerializeField]
    float _yPosition = 12f;
    [SerializeField]
    float _zPosition = 0f;
    [SerializeField]
    bool _spawnThings = true;
    [SerializeField]
    float _allEnemySpawnDelay = 2.0f;

    Vector3 _currentEnemyLookingDir;

    public int EnemyCount
    {
        get
        {
            return _enemyContainer.childCount;
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }

    //iEnumerate enemy spawns.
    IEnumerator spawnEnemyRoutine()
    {
        yield return new WaitForSeconds(_allEnemySpawnDelay);
        foreach(EnemySpawnConfiguration config in _enemiesToSpawn)
        {
            if (!_spawnThings)
                break;

            for(int i = 0; i < config.spawnCount; i++)
            {
                Vector3 offset = config.spawnOffsets[i];
                Instantiate(config.enemyPrefab, _spawners[config.spawnerID].position + offset, _spawners[config.spawnerID].rotation, _enemyContainer);
            }

            if (config.flag == PostWaveFlag.UntilAllClear)
            {
                while (EnemyCount > 0)
                {
                    yield return new WaitForSeconds(1);
                }
            }

            yield return new WaitForSeconds(config.delay);
        }
        Debug.Log("No more enemies remain.");
        while (EnemyCount > 0)
        {
            yield return new WaitForSeconds(1);
        }
        Debug.Log("All enemies eliminated");
        yield return null;
    }

    //iEnumerate powerup spawns.
    IEnumerator spawnPowerupRoutine()
    {
        yield return new WaitForSeconds(_powerUpSpawnDelay);
        while(_spawnThings)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_xRange.x, _xRange.y), _yPosition, _zPosition);
            Instantiate(_powerUpOptions[Random.Range(0, _powerUpOptions.Length)], spawnPos, Quaternion.identity, _powerUpContainer);
            yield return new WaitForSeconds(Random.Range(_powerUpSpawnRate.x, _powerUpSpawnRate.y));
        }
        yield return null;
    }

    //Catch the player death and stop spawning things!
    public void OnPlayerDeath()
    {
        _spawnThings = false;
    }

}
