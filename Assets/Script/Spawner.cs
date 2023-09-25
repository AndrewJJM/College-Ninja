 using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    private Collider spawnArea; 

    [SerializeField] private  GameObject[] PrefabOggetti;
    [SerializeField] private  GameObject bombaPrefab;
    [Range(0f, 1f)] public float bombaChance = 0.05f;

    [SerializeField] private float minSpawnDelay = 0.25f;
    [SerializeField] private float maxSpawnDelay = 1f;

    [SerializeField] private float minAngolo = -15f;
    [SerializeField] private float maxAngolo = 15f;

    [SerializeField] private float minForza = 18f;
    [SerializeField] private float maxForza= 22f;

    [SerializeField] private float maxLifetime = 5f;

    
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
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
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab =  PrefabOggetti[Random.Range(0, PrefabOggetti.Length)];

            if (Random.value < bombaChance) { 
                prefab = bombaPrefab;
            }

            Vector3 coordinate = new Vector3();
            coordinate.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            coordinate.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            coordinate.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            //angolo di lancio
            Quaternion rotazione = Quaternion.Euler(0f, 0f, Random.Range(minAngolo, maxAngolo));

            GameObject Oggetto = Instantiate(prefab, coordinate, rotazione);
            Destroy(Oggetto, maxLifetime);

            //forza di lancio
            float forza = Random.Range(minForza, maxForza);
            Oggetto.GetComponent<Rigidbody>().AddForce(Oggetto.transform.up * forza, ForceMode.Impulse);
            
            //rotazione random del gameObject sull'asse delle z
            //Oggetto.transform.rotation = Quaternion.Euler (Random.Range(-90f, 90f), 90f , 0f); 



            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    /*troy è stato qui*/
}