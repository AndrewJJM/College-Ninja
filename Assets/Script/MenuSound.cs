using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    private static MenuSound instance;
    private AudioSource audioSource;

    public bool isPlaying = false;

    public static MenuSound Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<MenuSound>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(MenuSound).Name;
                    instance = obj.AddComponent<MenuSound>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Recupera l'AudioSource esistente sull'oggetto GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("L'oggetto GameObject deve avere un componente AudioSource.");
        }
    }

    // Metodo per riprodurre un suono
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            isPlaying = true;
        }
        else
        {
            Debug.LogError("AudioSource non trovato sull'oggetto GameObject.");
        }
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            isPlaying = false;
        }
        else
        {
            Debug.LogError("AudioSource non trovato sull'oggetto GameObject.");
        }
    }

    // Esempio di utilizzo
    private void Start()
    {
        // Riproduci il primo suono all'avvio
        Instance.PlaySound();
    }
}
