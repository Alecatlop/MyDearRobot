using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravedadPuerta : MonoBehaviour
{
    public GameObject jugador;
    public GameObject puerta;
    void OnTriggerEnter(Collider other)
    {
        puerta.gameObject.SetActive(false);
    }
}
