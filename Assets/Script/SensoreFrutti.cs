using PlayFab.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SensoreFrutti : MonoBehaviour

{
    public GameManager gamemanager;

    [SerializeField] private GameObject missedSymbol;
    [SerializeField] private Transform parentCanvas;
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] AudioManager audioManager;

    private void Awake()
    {
        //_collider = GetComponent<BoxCollider>(); non serve?
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 ObjectScreenPosition = Camera.main.WorldToScreenPoint(other.transform.position);
        GameObject temp_symbol = GameObject.Instantiate(missedSymbol, parentCanvas);
        //aggiungere suono qui
        audioManager.PlasySFX(audioManager.fall);

        temp_symbol.transform.position = new Vector3(ObjectScreenPosition.x, temp_symbol.transform.position.y, temp_symbol.transform.position.z);
        Destroy(temp_symbol, 2);
        gamemanager.decreaseLife();
    }
}
