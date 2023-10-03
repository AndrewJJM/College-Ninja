using System;
using UnityEngine;

public class Oggetto : MonoBehaviour {
    
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
}
