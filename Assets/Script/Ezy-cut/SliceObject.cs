using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform planeDebug;
    public GameObject target;
    public Material CrossSection;
    public float ForzaTaglio = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Slice(target);
        }
    }

    private void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

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
