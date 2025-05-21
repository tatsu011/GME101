using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField]
    float _speed = 4f;
    [SerializeField] protected Direction _defaultFacingDirection = Direction.south;
    [Tooltip("Y sets the top plane's y position, X sets the randomness between X and -X (otherwise it just uses the curren X position.")]
    [SerializeField]
    Vector2 _topPlane;

    [SerializeField]
    float _bottomPlane;

    [SerializeField]
    int _points;

    [SerializeField]
    float _animationLength = 2.5f;

    [Header("Laser settings")]
    [SerializeField]
    float _minFireRate = 2.5f;
    [SerializeField]
    float _maxFireRate = 7f;
    [SerializeField]
    GameObject _laserPrefab;
    [SerializeField]
    Transform _laserSpawnPosition;
    [SerializeField]
    protected Vector2 _movingDirection = Vector2.down;

    protected Player _player;
    Animator _anim;
    AudioSource _audio;
    float _nextShotTime = 0f;
    Transform _laserContainer;
    bool _gettingDestroyed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindAnyObjectByType<Player>();
        if(_player == null)
        {
            Debug.LogError("Unable to find Player object.");
        }

        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Error: Enemy missing animator component!");
        }
        _audio = GetComponent<AudioSource>();
        if(_audio == null)
        {
            Debug.LogError("Error: no audio for death sfx.");
        }
        _laserContainer = GameObject.Find("LaserContainer").transform;
        
        _nextShotTime = Random.Range(_minFireRate, _maxFireRate) + Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();

        if (Time.time > _nextShotTime && !_gettingDestroyed)
        {
            FireLaser();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player?.Damage();
            _player?.OnEnemyKill(_points); //this is the easy version..
            TakeDamage();
        }
        if (other.tag == "playerProjectile")
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
            _player?.OnEnemyKill(_points); //this is equvalent to if(_player != null) _player.OnEnemyKill();
            TakeDamage();

        }
        if (other.tag == "playerExplosive")
        {
            TakeDamage();
        }
    }

    public virtual void DoMovement()
    {
        transform.Translate(_movingDirection * (_speed * Time.deltaTime));

        if (transform.position.y < _bottomPlane)
        {
            if (_topPlane.x <= 0) //no randomness
                transform.position = new Vector3(transform.position.x, _topPlane.y, transform.position.z);
            else
                transform.position = new Vector3(Random.Range(-_topPlane.x, _topPlane.x), _topPlane.y, transform.position.z);

        }
    }

    public virtual void FireLaser()
    {
        //spawn a laser, based off our position and rotation.
        if(_laserContainer != null)
            Instantiate(_laserPrefab, _laserSpawnPosition.position, Quaternion.FromToRotation(Vector3.up, Vector3.down), _laserContainer);
        else
            Instantiate(_laserPrefab, _laserSpawnPosition.position, transform.rotation);

        _nextShotTime = Random.Range(_minFireRate, _maxFireRate) + Time.time;

    }

    public virtual void TakeDamage()
    {
        OnDeath();
    }

    public virtual void OnDeath()
    {
        _anim.SetTrigger("Destroying");
        _audio.Play();
        _speed = 0f;
        Destroy(this.gameObject, _animationLength);
        _gettingDestroyed = true;
        GetComponent<Collider2D>().enabled = false;
    }

    public virtual void Initialize(int flags)
    {

    }

    public virtual Direction GetFacingDirection()
    {
        return _defaultFacingDirection;
    }
}

public enum Direction
{
    north, east, south, west
}
