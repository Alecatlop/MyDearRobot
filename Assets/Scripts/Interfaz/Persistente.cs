using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Persistente : MonoBehaviour
{
    public float valorcalidad;
    public float volumenmusica;
    public float volumenefectos;

    private static Persistente instancia;

    public AudioSource audioSource;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            volumenefectos = PlayerPrefs.GetFloat("efectos", 0.5f);
            volumenmusica = PlayerPrefs.GetFloat("musica", 0.5f);
            valorcalidad = PlayerPrefs.GetFloat("brillo", 0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GuardarDatos()
    {
        PlayerPrefs.SetFloat("efectos", volumenefectos);
        PlayerPrefs.SetFloat("musica", volumenmusica);
        PlayerPrefs.SetFloat("brillo", valorcalidad);
        PlayerPrefs.Save();
    }

    public void CambiarMusica(AudioClip nuevo)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio.clip != nuevo)
        {
            audio.clip = nuevo;
            audio.loop = true;
            audio.Play();
        }
    }
}
