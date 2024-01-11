using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------Audio Source-------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;



    [Header("-------Audio Clip-------")]
        public AudioClip background;
        public AudioClip slicing;
        public AudioClip death;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlasySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

 
