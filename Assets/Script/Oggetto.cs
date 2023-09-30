using System;
using UnityEngine;

public class Oggetto : MonoBehaviour {
    
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody objectRigidbody;
    private Collider objectCollider;
       
    private ParticleSystem Effect;

    public int points = 1;

 private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        // TODO: Effect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(points); //aggiungi punti

        // Disabilita l'intero oggetto
        objectCollider.enabled = false;
        whole.SetActive(false);

        // Abilita l'oggetto tagliato 
        sliced.SetActive(true);
        //TODO: Effect.Play();

        // Ruota in base all'angolo di taglio 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        // Aggiungi una forza a ogni fetta in base alla direzione della lama 
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = objectRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }
 private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.ForzaTaglio);
        }
    }
}
