using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player stats")]
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    int lives = 4;

    [Header("Communication Settings.")]
    [SerializeField]
    SpawnManager spawnManager;

    [Header("Screen bounds")]
    [SerializeField]
    float upperBounds = 5f;
    [SerializeField]
    float lowerBounds = -5f;
    [SerializeField]
    float rightBounds = 12f;
    [SerializeField]
    float leftBounds = -12f;
    [SerializeField]
    bool wrapAround = true;

    [Header("Laser Settings")]
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private Transform laserContainer;
    [SerializeField]
    private Transform laserSpawnPosition;
    [SerializeField]
    private float fireRate = 2.5f;
    [SerializeField]
    float _whenCanFire = -1;



    Vector3 position;
    Vector3 motion;

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

    private void FireLaser()
    {
        if (laserSpawnPosition != null)
            Instantiate(laserPrefab, laserSpawnPosition.position, Quaternion.identity, laserContainer.transform);
        else
            Instantiate(laserPrefab, transform.position, Quaternion.identity, laserContainer.transform);
        _whenCanFire = Time.time + fireRate;
    }

    private void BoundsCheck()
    {
        position = transform.position;

        if (transform.position.y < lowerBounds)
            position.y = lowerBounds;
        if (transform.position.y > upperBounds)
            position.y = upperBounds;
        if (transform.position.x < leftBounds)
            position.x = wrapAround ? rightBounds : leftBounds;
        if (transform.position.x > rightBounds)
            position.x = wrapAround ? leftBounds : rightBounds;
        transform.position = position;
    }

    public void UpdateBounds(Vector4 newBounds)
    {

    }

    private void DoMovement()
    {
        motion.x = Input.GetAxis("Horizontal");
        motion.y = Input.GetAxis("Vertical");
        motion.z = 0f;
        transform.Translate(motion * (Time.deltaTime * speed));
    }

    public void Damage()
    {
        lives--;
        if(lives <= 0)
        {
            spawnManager.OnPlayerDeath(); //todo: event.
            Destroy(gameObject);
        }
    }
}
