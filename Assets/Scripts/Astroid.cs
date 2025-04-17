using UnityEngine;
using UnityEngine.Rendering;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    float _spinSpeed;
    [SerializeField]
    GameObject _explosionPrefab;
    [SerializeField]
    float _explosionDuration = 2.5f;
    SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _spinSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("playerProjectile"))
        {
            Destroy(Instantiate(_explosionPrefab, transform.position, Quaternion.identity), _explosionDuration);
            Destroy(collision.gameObject);
            _spawnManager.StartSpawning();
            Destroy(gameObject);

        }
    }
}
