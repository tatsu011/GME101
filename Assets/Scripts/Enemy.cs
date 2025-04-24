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

    [SerializeField]
    bool destroying = false;

    [SerializeField]
    float animationLength = 2.5f;

    Player _player;
    Animator _anim;
    AudioSource _audio;

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
            _anim.SetTrigger("Destroying");
            _audio.Play();
            _speed = 0f;
            Destroy(this.gameObject, animationLength);
            GetComponent<Collider2D>().enabled = false;
            destroying = true;
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
            _anim.SetTrigger("Destroying");
            _audio.Play();
            _speed = 0f;
            Destroy(gameObject, animationLength);
            GetComponent<Collider2D>().enabled = false;
            destroying = true;

        }
    }
}
