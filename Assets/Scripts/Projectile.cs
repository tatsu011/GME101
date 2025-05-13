using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float _speed;
    [SerializeField]
    float _lifetime = 10;
    [SerializeField]
    GameObject _spawnableObject;
    [SerializeField]
    float _spawnedObjectLifetime;
    [SerializeField]
    bool _spawnedObjectImpactEffect = false;

    float life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = Time.time + _lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));

        if (life < Time.time)
        {
            if (transform.parent != null && transform.parent.tag != "container") //Don't break the container please.
                Destroy(transform.parent.gameObject);

            if (_spawnableObject != null) //spawn whatever payload is in the object.
            {
                if(_spawnedObjectImpactEffect) Camera.main.GetComponent<CameraEffects>().DoCameraShake();

                Destroy(Instantiate(_spawnableObject, transform.position, transform.rotation), _spawnedObjectLifetime);
            }
            Destroy(gameObject);
        }

    }
}
