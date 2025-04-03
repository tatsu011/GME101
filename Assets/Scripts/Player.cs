using System;
using System.Collections;
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

    float _boostedSpeed = 1.0f;
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
            Instantiate(_laserPrefabs[_lasers - 1], _laserSpawnPosition.position, Quaternion.identity, _laserContainer.transform);
        else
            Instantiate(_laserPrefabs[_lasers - 1], transform.position, Quaternion.identity, _laserContainer.transform);
        _whenCanFire = Time.time + _fireRate;
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

    internal void ActivatePowerup(PowerupType powerType)
    {
        switch (powerType)
        {
            case PowerupType.ShotUp:
                _lasers++;
                StartCoroutine(LaserPowerDownRoutine());
                break;
            case PowerupType.ShieldUp:
                //StartCoroutine(ShieldPowerDownRoutine());
                break;
            case PowerupType.SpeedUp:
                _boostedSpeed = _speedBoost;
                StartCoroutine(SpeedPowerDownRoutine());
                break;
            default:
                break;
        }
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
