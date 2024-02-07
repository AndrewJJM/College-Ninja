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
        public AudioClip fall;
        public AudioClip multiplayer;
        public AudioClip gameOver;
        public AudioClip genericAudio;

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

 
