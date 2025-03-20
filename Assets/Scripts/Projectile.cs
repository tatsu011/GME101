using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float _speed;
    [SerializeField]
    float _lifetime = 10;

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
            Destroy(gameObject);

    }
}
