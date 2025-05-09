using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baldosa : MonoBehaviour
{
    public bool correcto;
    public Renderer runarenderer;
    float velocidadCaida = 10f;

    private bool caer = false;
    private Vector3 posicionObjetivo;

    void Start()
    {
        // Inicialmente no hay posición objetivo
        posicionObjetivo = transform.position;
    }

    void Update()
    {
        if (caer)
        {
            // Mover suavemente hacia la posición objetivo
            transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo, velocidadCaida * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            Material material = runarenderer.material;

            if (correcto)
            {
                Debug.Log("hola");
                material.SetColor("_EmissionColor", Color.green);
                material.EnableKeyword("_EMISSION");
            }
            else
            {
                material.SetColor("_EmissionColor", Color.red);
                material.EnableKeyword("_EMISSION");

                // Establece la nueva posición objetivo más abajo
                posicionObjetivo = transform.position + Vector3.down * 100f; // Puedes ajustar la distancia
                caer = true;
            }
        }
    }
}
