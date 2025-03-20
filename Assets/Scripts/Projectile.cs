using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float lifetime = 10;

    float life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));

        if (life < Time.time)
            Destroy(gameObject);

    }
}
