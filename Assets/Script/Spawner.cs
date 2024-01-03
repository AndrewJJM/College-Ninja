using JetBrains.Annotations;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    private Collider spawnArea; 

    [SerializeField] private  GameObject[] PrefabOggetti;
    [SerializeField] private  GameObject bombaPrefab;
    [Range(0f, 1f)] public float bombaChance = 0.05f;

    [SerializeField] private float minSpawnDelay = 0.15f;
    [SerializeField] private float maxSpawnDelay = 3f;

    [SerializeField] private float minAngolo = -15f;
    [SerializeField] private float maxAngolo = 15f;

    [SerializeField] private float minForza = 18f;
    [SerializeField] private float maxForza= 22f;

    [SerializeField] private float maxLifetime = 5f;

    //difficulty stats
    [SerializeField] private float difficultyTimer = 30f;
    [SerializeField] private float TargetBombChance = 0.445f;
    [SerializeField] private float targetMaxSpawnDelay = 0.5f;




    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
        StartCoroutine(IncrementaOgniTotSecondi());
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
            Oggetto.tag = "Sliceable";

            Destroy(Oggetto, maxLifetime);

            //forza di lancio
            float forza = Random.Range(minForza, maxForza);
            Oggetto.GetComponent<Rigidbody>().AddForce(Oggetto.transform.up * forza, ForceMode.Impulse);
            Oggetto.GetComponent<Rigidbody>().AddTorque(Random.Range(-80f, 80f), Random.Range(-80f, 80f), 0f);


            //rotazione random del gameObject sull'asse delle y
            //Oggetto.transform.Rotate(Vector3.up * 30f * Time.deltaTime);



            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));


        }


    }
    private IEnumerator IncrementaOgniTotSecondi()
    {


            // Incrementa il valore finché non raggiunge il massimo e minimo per rendere più difficile il gioco
            while (maxSpawnDelay > targetMaxSpawnDelay || bombaChance < TargetBombChance)
            {
                if (maxSpawnDelay > targetMaxSpawnDelay)
                {
                    maxSpawnDelay -= 0.50f;
                }
                if (bombaChance < TargetBombChance)
                {
                    bombaChance += 0.05f;
                }

            yield return new WaitForSeconds(difficultyTimer);
            }
    }
    /*troy è stato qui*/
}