using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 4f;

    [Tooltip("Y sets the top plane's y position, X sets the randomness between X and -X (otherwise it just uses the curren X position.")]
    [SerializeField]
    Vector2 _topPlane;

    [SerializeField]
    float _bottomPlane;

    [SerializeField]
    int points;

    Player _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));

        if(transform.position.y < _bottomPlane)
        {
            if (_topPlane.x <= 0) //no randomness
                transform.position = new Vector3(transform.position.x, _topPlane.y, transform.position.z);
            else
                transform.position = new Vector3(Random.Range(-_topPlane.x, _topPlane.x), _topPlane.y, transform.position.z);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") 
        {
            other.GetComponent<Player>()?.Damage();
            other.GetComponent<Player>()?.OnEnemyKill(points); //this is the easy version..
            Destroy(this.gameObject);
        }
        if(other.tag == "playerProjectile")
        {
            if (other.gameObject.transform.parent.tag != "container" && other.transform.parent.childCount == 1)
            {
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
            //this is the hard version which is more common...
            _player?.OnEnemyKill(points); //this is equvalent to if(_player != null) _player.OnEnemyKill();
            Destroy(gameObject);
        }
    }
}
