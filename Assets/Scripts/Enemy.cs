using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;

    [SerializeField]
    Vector3 topPlane;

    [SerializeField]
    Vector3 bottomPlane;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));

        if(transform.position.y < bottomPlane.y)
        {
            if (topPlane.x <= 0) //no randomness
                transform.position = new Vector3(transform.position.x, topPlane.y, transform.position.z);
            else
                transform.position = new Vector3(Random.Range(-topPlane.x, topPlane.x), topPlane.y, transform.position.z);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") 
        {
            other.GetComponent<Player>()?.Damage();
            Destroy(this.gameObject);
        }
        if(other.tag == "playerProjectile")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
