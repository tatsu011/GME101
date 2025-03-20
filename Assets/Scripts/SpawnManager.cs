using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    bool _spawnEnemies = true;

    [SerializeField]
    GameObject _enemyToSpawn;

    [SerializeField]
    Transform _parentObject;

    [SerializeField]
    float _spawnDelay = 5f;

    [Tooltip("sets a range.  min: x, max: y")]
    [SerializeField]
    Vector2 _XRange = new Vector2(-8f, 8f);
    [SerializeField]
    float _yPosition = 12f;
    [SerializeField]
    float _zposition = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //iEnumerate spawn rate.
    IEnumerator spawnEnemy()
    {
        while(_spawnEnemies) 
        {
            Vector3 spawnPos = new Vector3(Random.Range(_XRange.x, _XRange.y), _yPosition, _zposition);
            Instantiate(_enemyToSpawn, spawnPos, Quaternion.identity, _parentObject);
            yield return new WaitForSeconds(_spawnDelay); //this happens last
        }

        yield return null;
    }

    public void OnPlayerDeath()
    {
        _spawnEnemies = false;
    }

}
