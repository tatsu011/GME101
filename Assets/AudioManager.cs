using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;

    static AudioManager _instance;

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        
    }


    public void PlaySound(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}

