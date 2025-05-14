using UnityEngine;

public interface IEnemy
{
    void DoMovement();
    void FireLaser();
    void TakeDamage();
    void OnDeath();
}
