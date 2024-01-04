using UnityEngine;

public class Bomba : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            FindAnyObjectByType<GameManager>().Explode();
        }
    }

}