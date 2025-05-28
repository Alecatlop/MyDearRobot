using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_2 : MonoBehaviour
{
    bool avanzar = false;
    bool retroceder = false;
    public float speed;
    public Transform centroCupula; 

    private Vector3 direccion;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume *= 0.5f;

        if (centroCupula != null)
        {
            // Calcula la dirección hacia el centro en el plano XZ
            Vector3 centroEnMismoNivel = new Vector3(centroCupula.position.x, transform.position.y, centroCupula.position.z);
            direccion = (centroEnMismoNivel - transform.position).normalized;

            // Orienta la plataforma mirando hacia el centro
            transform.rotation = Quaternion.LookRotation(direccion);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el centro de la cúpula en la plataforma " + gameObject.name);
        }
    }

    void OnEnable()
    {
        StartCoroutine(Movimiento());
    }

    void Update()
    {
        if (Pausa.juegoPausado) return;

        // Actualiza volumen con el valor actual del slider
        float volumenGlobal = PlayerPrefs.GetFloat("efectos", 1f);
        audioSource.volume = volumenGlobal * 0.5f;  
        
        if (avanzar || retroceder)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        
        if (avanzar)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (retroceder)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
    }

    private IEnumerator Movimiento()
    {
        while (true) 
        {
            avanzar = true;
            yield return new WaitForSeconds(1.5f);

            avanzar = false;
            yield return new WaitForSeconds(3f);

            retroceder = true;
            yield return new WaitForSeconds(1.5f);

            retroceder = false;
            yield return new WaitForSeconds(3f);
        }
    }
}