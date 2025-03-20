using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player stats")]
    [SerializeField]
    float _speed = 5f;
    [SerializeField]
    int _lives = 4;

    [Header("Communication Settings.")]
    [SerializeField]
    SpawnManager _spawnManager;

    [Header("Screen bounds")]
    [SerializeField]
    float _upperBounds = 5f;
    [SerializeField]
    float _lowerBounds = -5f;
    [SerializeField]
    float _rightBounds = 12f;
    [SerializeField]
    float _leftBounds = -12f;
    [SerializeField]
    bool _wrapAround = true;

    [Header("Laser Settings")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Transform _laserContainer;
    [SerializeField]
    private Transform _laserSpawnPosition;
    [SerializeField]
    private float _fireRate = 2.5f;
    [SerializeField]
    float _whenCanFire = -1;



    Vector3 _position;
    Vector3 _motion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();

        BoundsCheck();

        if (Input.GetKey(KeyCode.Space) && _whenCanFire < Time.time)
        {
            FireLaser();
        }
    }

    private void DoMovement()
    {
        _motion.x = Input.GetAxis("Horizontal");
        _motion.y = Input.GetAxis("Vertical");
        _motion.z = 0f;
        transform.Translate(_motion * (Time.deltaTime * _speed));
    }

    private void BoundsCheck()
    {
        _position = transform.position;

        if (transform.position.y < _lowerBounds)
            _position.y = _lowerBounds;
        if (transform.position.y > _upperBounds)
            _position.y = _upperBounds;
        if (transform.position.x < _leftBounds)
            _position.x = _wrapAround ? _rightBounds : _leftBounds; //if wrapAround is checked, move the player to the right-hand side of the screen. else: keep at left
        if (transform.position.x > _rightBounds)
            _position.x = _wrapAround ? _leftBounds : _rightBounds; //if wrapAround is checked, move the player to the left-hand side of the screen. else: keep at right
        transform.position = _position;
    }

    private void FireLaser()
    {
        if (_laserSpawnPosition != null)
            Instantiate(_laserPrefab, _laserSpawnPosition.position, Quaternion.identity, _laserContainer.transform);
        else
            Instantiate(_laserPrefab, transform.position, Quaternion.identity, _laserContainer.transform);
        _whenCanFire = Time.time + _fireRate;
    }

    public void UpdateBounds(Vector4 newBounds)
    {
        //for future implementation.  
    }

    public void Damage()
    {
        _lives--;
        if(_lives <= 0)
        {
            _spawnManager.OnPlayerDeath(); //todo: event.
            Destroy(gameObject);
        }
    }
}
