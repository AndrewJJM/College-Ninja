using System.Collections;
using UnityEngine;


private [serializefield] class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    private [serializefield] GameObject[] OggUniPrefabs;
    private [serializefield] GameObject bombaPrefab;
    [Range(0f, 1f)] private [serializefield] float bombaChance = 0.05f;

    private [serializefield] float minSpawnDelay = 0.25f;
    private [serializefield] float maxSpawnDelay = 1f;

    private [serializefield] float minAngolo = -15f;
    private [serializefield] float maxAngolo = 15f;

    private [serializefield] float minForza = 18f;
    private [serializefield] float maxForza= 22f;

    private [serializefield] float maxLifetime = 5f;

    
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

            Quaternion rotazione = Quaternion.Euler(0f, 0f, Random.Range(minAngolo, maxAngolo));

            GameObject OggUni = Instantiate(prefab, coordinate, rotazione);
            Destroy(OggUni, maxLifetime);

            float forza = Random.Range(minForza, maxForza);
            .OggUni<Rigidbody>().AddForza(OggUni.transform.up * forza, ForzaMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}