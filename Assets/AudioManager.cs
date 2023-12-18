using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source-----")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------Audio Clip-------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;

}
