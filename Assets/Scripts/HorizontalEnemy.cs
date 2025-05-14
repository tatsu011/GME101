using UnityEngine;

public class HorizontalEnemy : Enemy
{
    [SerializeField]
    float _borderDistance;
    [SerializeField]
    SpriteRenderer _renderObject;
    [SerializeField]
    bool _reverseDirection;
    [SerializeField]
    GameObject _explosionPrefab;
    [SerializeField]
    float _explosionDuration = 2.5f;

    public override void DoMovement()
    {
        base.DoMovement();
        if(Mathf.Abs(transform.position.x) > _borderDistance)
        {
            FlipDirection();
        }

    }

    public override void Initialize(int flags)
    {
        if(flags == 1)
            FlipDirection();
    }

    void FlipDirection()
    {
        _movingDirection = _movingDirection * -1;
        _renderObject.flipY = !_renderObject.flipY; //invert the flip based off the original sprite's Y Axis.
    }
    public override void OnDeath()
    {
        //no base, we just doing our own thing.
        Destroy(Instantiate(_explosionPrefab, transform.position, transform.rotation), _explosionDuration);
        Destroy(gameObject);
    }

}
