using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    bool SpawnEnemies = true;

    [SerializeField]
    GameObject enemyToSpawn;

    [SerializeField]
    Transform parentObject;

    [SerializeField]
    float spawnDelay = 5f;

    [SerializeField]
    Vector2 XRange = new Vector2(-8f, 8f);
    [SerializeField]
    float ypos = 12f;
    [SerializeField]
    float zpos = 0f;

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
        while(SpawnEnemies)
        {
            yield return new WaitForSeconds(spawnDelay);
            Vector3 spawnPos = new Vector3(Random.Range(XRange.x, XRange.y), ypos, zpos);
            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity, parentObject);
        }

        yield return null;
    }

    public void OnPlayerDeath()
    {
        SpawnEnemies = false;
    }

}
