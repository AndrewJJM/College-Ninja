using Unity.VisualScripting;
using UnityEngine;
using EzySlice;


public class Blade : MonoBehaviour
{
    public Vector3 direction { get; private set; } //Lasciare pubblico

    private Camera mainCamera;

    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public float minSliceVelocity = 0.01f;

    private bool slicing;

    public Transform inizio_lama;
    public Transform fine_lama;

    [SerializeField] private float maxLifetime = 2f;

    AudioManager audioManager;

    //importato da sliceObject
    public VelocityEstimator velocityEstimator;
    public Material CrossSection;
    public float ForzaTaglio = 10f;


    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        StopSlice();
    }

    private void OnDisable()
    {
        StopSlice();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if (slicing)
        {
            ContinueSlice();
        }
    }

    private Vector3 StartSlice()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
        return this.transform.position;
    }

    private void StopSlice()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlice()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;


        direction = newPosition - transform.position;
        sliceCollider.enabled = direction != Vector3.zero;

        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            //Blade blade = other.GetComponent<Blade>();
            Slice(other.gameObject);
        }
    }

    private void Slice(GameObject target)
    {
        Vector3 velocita = velocityEstimator.GetVelocityEstimate();
        Vector3 normalePiano = Vector3.Cross(inizio_lama.position - fine_lama.position, velocita);
        normalePiano.Normalize();
        
        SlicedHull hull = target.Slice(fine_lama.position, normalePiano);

        if (hull != null)
        {
            GameObject[] slices = new GameObject[2];
            slices[0] = hull.CreateUpperHull(target, CrossSection);
            slices[1] = hull.CreateLowerHull(target, CrossSection);

            Destroy(target);
            ImpostaTaglio(slices, this.direction, this.transform.position);
            FindAnyObjectByType<GameManager>().IncreaseScore(1);
            audioManager.PlasySFX(audioManager.slicing);
        }

    }

    private void ImpostaTaglio(GameObject[] pezziTagliati, Vector3 direzione, Vector3 posizione)
    {
        foreach (GameObject slice in pezziTagliati)
        {
            Rigidbody rb = slice.AddComponent<Rigidbody>();
            MeshCollider collider = slice.AddComponent<MeshCollider>();
            collider.convex = true;
            //collider.isTrigger = true; // dopo averlo tagliato, non interagisce pi� fisicamente: non funziona
            //rb.AddExplosionForce(ForzaTaglio, slice.transform.position, 1);   implementazione vecchia
            rb.AddForceAtPosition(direzione.normalized * ForzaTaglio, posizione, ForceMode.Impulse);
            Destroy(slice, maxLifetime);
        }
        
    }
}
