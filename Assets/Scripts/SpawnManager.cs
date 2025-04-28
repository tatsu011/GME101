using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [Header("Enemy Spawn Settings")]
    [SerializeField]
    GameObject _enemyToSpawn;
    [SerializeField]
    Transform _enemyContainer;
    [SerializeField]
    float _enemySpawnRate = 5f;

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

    public void StartSpawning()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }

    //iEnumerate enemy spawns.
    IEnumerator spawnEnemyRoutine()
    {
        yield return new WaitForSeconds(_allEnemySpawnDelay);
        while(_spawnThings) 
        {
            Vector3 spawnPos = new Vector3(Random.Range(_xRange.x, _xRange.y), _yPosition, _zPosition);
            Instantiate(_enemyToSpawn, spawnPos, Quaternion.identity, _enemyContainer);
            yield return new WaitForSeconds(_enemySpawnRate); //this happens last
        }

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
