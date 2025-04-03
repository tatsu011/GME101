using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float _dropSpeed = 3f;
    [SerializeField]
    float _bottom;

    [SerializeField]
    PowerupType _powerType;

    private void Update()
    {
        transform.Translate(Vector3.down * (_dropSpeed * Time.deltaTime));
        if (transform.position.y < _bottom)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player == null)
                return;
            player.ActivatePowerup(_powerType);
            Destroy(gameObject);
        }
    }
}

public enum PowerupType
{
    ShotUp,ShieldUp,SpeedUp
}
