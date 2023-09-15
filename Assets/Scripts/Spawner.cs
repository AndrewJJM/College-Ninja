using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    
    public GameObject[] fruitPrefabs;
    
    public float minSoawnDelay= 0.25f;
    public float maxSpawnDelay= 1f;
    
    public float minAngle = -15f;
    public float maxAngle= 15f;
    
    public float minForce=18f;
    public float maxforce=22f;
    
    public float maxLifetime= 5f;
    
    private void Awake()
    {
      spawnArea = GetComponent <Collider>();
    }

 private void OnEnable()
{
  StartCoroutine(Spawn());
}

private void OnDisable()
{
    StopAllCoroutines();
}

private IEnumerator Spawn()
{
    
}

}
