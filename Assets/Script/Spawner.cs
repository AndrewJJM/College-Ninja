using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    [SerializeField] private  GameObject[] OggUniPrefabs;
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
            GameObject prefab = OggUniPrefabs[Random.Range(0, OggUniPrefabs.Length)];

            if (Random.value < bombaChance) { 
                prefab = bombaPrefab;
            }

            Vector3 coordinate = new Vector3();
            coordinate.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            coordinate.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            coordinate.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotazione = Quaternion.Euler(0f, 90f, Random.Range(minAngolo, maxAngolo));

            GameObject OggUni = Instantiate(prefab, coordinate, rotazione);
            Destroy(OggUni, maxLifetime);

            float forza = Random.Range(minForza, maxForza);
            OggUni.GetComponent<Rigidbody>().AddForce(OggUni.transform.up * forza, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}