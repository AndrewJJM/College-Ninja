using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensoreFrutti : MonoBehaviour

{
    public GameManager gamemanager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sliceable")
        {
            gamemanager.Explode(); //da impostare un counter con 3 vite
        }
    }
}
