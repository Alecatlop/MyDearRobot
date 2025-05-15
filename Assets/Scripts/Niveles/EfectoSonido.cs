using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    private AudioSource[] audios;
    [Range(0f, 1f)]
    public float volumenBase = 1f; 

    void Awake()
    {
        audios = GetComponents<AudioSource>();
    }

    void Start()
    {
        float volumenGlobal = PlayerPrefs.GetFloat("efectos", 1f);
        SetVolumen(volumenGlobal);
    }

    public void SetVolumen(float volumen)
    {
        foreach (AudioSource audio in audios)
        {
            audio.volume = volumen * volumenBase;
        }
    }

    public void Pausar()
    {
        foreach (AudioSource audio in audios)
        {
            if (audio.isPlaying)
                audio.Pause();
        }
    }

    public void Reanudar()
    {
        foreach (AudioSource audio in audios)
        {
            audio.UnPause();
        }
    }
}
