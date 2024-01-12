using PlayFab.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensoreFrutti : MonoBehaviour

{
    [SerializeField] private LayerMask layermask;
    public GameManager gamemanager;
    private Camera _camera;
    private BoxCollider _collider;

    private void Awake()
    {
        _camera = Camera.main;
        _collider = GetComponent<BoxCollider>();
        Debug.Log(_camera.pixelHeight);
        Debug.Log(_camera.pixelWidth);
        setAreaSize();
    }

    private void setAreaSize()
    {
        _collider.size = new Vector3(_camera.aspect * 2f * _camera.orthographicSize, 2f * _camera.orthographicSize, 20f);
        _collider.center = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Something");
    }
    private void OnCollisionExit(Collision collision)
    {
        gamemanager.Explode(); //da impostare un counter con 3 vite
    }
}
