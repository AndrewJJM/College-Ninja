using Unity.VisualScripting;
using UnityEngine;
using EzySlice;

public class Blade : MonoBehaviour
{
    public Vector3 direction { get; private set; } //Lasciare pubblico

    private Camera mainCamera;

    private Collider sliceCollider;
    private TrailRenderer sliceTrail;

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;

    private bool slicing;

    public Transform inizio_lama;
    public Transform fine_lama;


    //importato da sliceObject
    public VelocityEstimator velocityEstimator;
    public Material CrossSection;
    public float ForzaTaglio = 2000f;


    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
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

    private void StartSlice()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;
        sliceTrail.Clear();
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

        float velocity = direction.magnitude / Time.deltaTime;
        sliceCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Blade blade = other.GetComponent<Blade>();
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
            GameObject upperHull = hull.CreateUpperHull(target, CrossSection);
            ImpostaTaglio(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, CrossSection);
            ImpostaTaglio(lowerHull);
            Destroy(target);
        }
    }

    private void ImpostaTaglio(GameObject pezzoTagliato)
    {
        Rigidbody rb = pezzoTagliato.AddComponent<Rigidbody>();
        MeshCollider collider = pezzoTagliato.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(ForzaTaglio, pezzoTagliato.transform.position, 1);
    }
}
