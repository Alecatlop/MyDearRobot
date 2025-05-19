using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baldosa : MonoBehaviour
{
    public bool correcto;
    public Renderer runarenderer;
    float velocidadCaida = 10f;

    public AudioClip RunaIncorrecta;
    public AudioClip RunaCorrecta;
    private AudioSource audioSource;
    private bool sonidoCorrectoReproducido = false;
    private bool caer = false;
    private Vector3 posicionObjetivo;

    void Start()
    {
        // Inicialmente no hay posición objetivo
        posicionObjetivo = transform.position;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f; 
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

                if (!sonidoCorrectoReproducido && RunaCorrecta != null)
                {
                    audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;
                    audioSource.PlayOneShot(RunaCorrecta);
                    sonidoCorrectoReproducido = true;
                }
            }
            else
            {
                material.SetColor("_EmissionColor", Color.red);
                material.EnableKeyword("_EMISSION");

                if (RunaIncorrecta != null)
                {
                    audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;
                    audioSource.PlayOneShot(RunaIncorrecta);
                }

                // Establece la nueva posición objetivo más abajo
                posicionObjetivo = transform.position + Vector3.down * 100f; // Puedes ajustar la distancia
                caer = true;
            }
        }
    }
}
