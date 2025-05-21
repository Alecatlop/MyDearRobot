using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarDesactivarSonidoZona : MonoBehaviour
{
    [Header("GameObjects a activar")]
    public GameObject[] objetosAActivar;

    [Header("GameObjects a desactivar")]
    public GameObject[] objetosADesactivar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activamos los objetos nuevos
            foreach (GameObject obj in objetosAActivar)
            {
                obj.SetActive(true);

                // Reproducimos el sonido si tiene
                AudioSource audio = obj.GetComponent<AudioSource>();
                if (audio != null && !audio.isPlaying)
                {
                    audio.Play();
                }
            }

            // Desactivamos objetos anteriores
            foreach (GameObject obj in objetosADesactivar)
            {
                // Detenemos el sonido si tiene
                AudioSource audio = obj.GetComponent<AudioSource>();
                if (audio != null && audio.isPlaying)
                {
                    audio.Stop();
                }

                obj.SetActive(false);
            }

            // Destruimos el trigger
            Destroy(gameObject); 
        }
    }
}
