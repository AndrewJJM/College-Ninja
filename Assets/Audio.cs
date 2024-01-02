using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectsWithTag("Audio").GetComponent<AudioManager>();
    }

   ...

    audioManager.PlaySFX(audioManager.death);
    audioManager.PlaySFX(audioManager.slice);


}
