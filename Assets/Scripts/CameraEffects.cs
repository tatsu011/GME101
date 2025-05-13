using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    public void DoCameraShake()
    {
        _animator.SetTrigger("Shimmy");
    }
}
