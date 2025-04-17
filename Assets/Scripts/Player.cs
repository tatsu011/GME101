using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Settings")]
    [SerializeField]
    float _speed = 5f;
    [SerializeField]
    int _lives = 3;
    [SerializeField]
    Transform[] _damageVisuals;
    [SerializeField]
    Transform _thrusterVisual;

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
    private GameObject[] _laserPrefabs;
    [SerializeField]
    private Transform _laserContainer;
    [SerializeField]
    private Transform _laserSpawnPosition;
    [SerializeField]
    private float _fireRate = 2.5f;
    [SerializeField]
    float _whenCanFire = -1;
    [SerializeField]
    int _lasers = 1;

    [Header("PowerupSettings")]
    [SerializeField]
    float _powerupCooldown = 5f;
    [SerializeField]
    float _speedBoost = 2.5f;
    [SerializeField]
    bool _activeShield = false;
    [SerializeField]
    Transform _shieldVisual;
    [SerializeField]
    int _score = 0;

    float _boostedSpeed = 1.0f;
    Vector3 _position;
    Vector3 _motion;
    GameObject _activeLaserInstance;
    int _lastDamageID = -1;

    Coroutine _laserCoroutine;
    Coroutine _SpeedCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shieldVisual.gameObject.SetActive(false);
        UIManager.Instance.UpdateScore(_score);
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
        transform.Translate(_motion * (Time.deltaTime * _speed * _boostedSpeed));
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
        if (_lasers > 5)
            _lasers = 5;
        if (_lasers < 1)
            _lasers = 1;

        if(_lasers == 1)
            _activeLaserInstance = Instantiate(_laserPrefabs[_lasers - 1], _laserSpawnPosition.position, Quaternion.identity, _laserContainer.transform);
        else
            _activeLaserInstance = Instantiate(_laserPrefabs[_lasers - 1], transform.position, Quaternion.identity, _laserContainer.transform);

        foreach (Projectile p in _activeLaserInstance.GetComponentsInChildren<Projectile>(true))
            p.setOwner(gameObject);

        _whenCanFire = Time.time + _fireRate;
    }

    public void Damage()
    {
        if(_activeShield)
        {
            DeactivateShields();
            return;
        }

        _lives--;
        if(_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            UIManager.Instance.OnPlayerDeath();
            Destroy(gameObject);
        }
        UIManager.Instance.UpdateLives(_lives);
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        if (_lastDamageID < 0)
            _lastDamageID = Random.Range(0, _damageVisuals.Length);


        _damageVisuals[_lastDamageID].gameObject.SetActive(true);
    }

    internal void ActivatePowerup(PowerupType powerType)
    {
        switch (powerType)
        {
            case PowerupType.ShotUp:
                _lasers++;
                if (_laserCoroutine != null)
                    StopCoroutine(_laserCoroutine); //reset the laser timer in addition to the boost.
                _laserCoroutine = StartCoroutine(LaserPowerDownRoutine());
                break;
            case PowerupType.ShieldUp:
                ActivateShields();
                break;
            case PowerupType.SpeedUp:
                _boostedSpeed = _speedBoost;
                if (_SpeedCoroutine != null)
                    StopCoroutine(_SpeedCoroutine); //Reset the speed timer when speed is picked up.
                _SpeedCoroutine = StartCoroutine(SpeedPowerDownRoutine());
                break;
            default:
                break;
        }
    }

    void ActivateShields()
    {
        _activeShield = true;
        _shieldVisual.gameObject.SetActive(true);
    }

    void DeactivateShields()
    {
        _activeShield = false;
        _shieldVisual.gameObject.SetActive(false);
    }

    public void OnEnemyKill(int value)
    {
        _score += value;
        UIManager.Instance.UpdateScore(_score);
    }

    IEnumerator LaserPowerDownRoutine()
    {
        while(_lasers > 1)
        {
            yield return new WaitForSeconds(_powerupCooldown);
            _lasers--;
        }
        yield return null; 
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        while(_boostedSpeed != 1)
        {
            yield return new WaitForSeconds(_powerupCooldown);
            _boostedSpeed = 1;
        }
        yield return null;
    }
}
