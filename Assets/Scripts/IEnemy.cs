using UnityEngine;

public interface IEnemy
{
    void Initialize(int flags);
    void DoMovement();
    void FireLaser();
    void TakeDamage();
    void OnDeath();
}
