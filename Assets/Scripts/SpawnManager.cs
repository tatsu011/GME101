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
    float _enemySpawnDelay = 5f;

    [Header("Powerup Spawn Settings")]
    [Tooltip("sets a rate in seconds to spawn powerups.  min: x, max: y")]
    [SerializeField]
    Vector2 _powerUpSpawnRate = new Vector2(3f, 7f);
    [SerializeField]
    GameObject[] _powerUpOptions;
    [SerializeField]
    Transform _powerUpContainer;


    [Header("General Spawn Settings")]
    [Tooltip("sets a range.  min: x, max: y")]
    [SerializeField]
    Vector2 _XRange = new Vector2(-8f, 8f);
    [SerializeField]
    float _yPosition = 12f;
    [SerializeField]
    float _zposition = 0f;
    [SerializeField]
    bool _spawnThings = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //iEnumerate enemy spawns.
    IEnumerator spawnEnemyRoutine()
    {
        while(_spawnThings) 
        {
            Vector3 spawnPos = new Vector3(Random.Range(_XRange.x, _XRange.y), _yPosition, _zposition);
            Instantiate(_enemyToSpawn, spawnPos, Quaternion.identity, _enemyContainer);
            yield return new WaitForSeconds(_enemySpawnDelay); //this happens last
        }

        yield return null;
    }

    //iEnumerate powerup spawns.
    IEnumerator spawnPowerupRoutine()
    {
        while(_spawnThings)
        {
            Vector3 spawnPos = new Vector3(Random.Range(_XRange.x, _XRange.y), _yPosition, _zposition);
            Instantiate(_powerUpOptions[Random.Range(0, _powerUpOptions.Length - 1)], spawnPos, Quaternion.identity, _powerUpContainer);
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
