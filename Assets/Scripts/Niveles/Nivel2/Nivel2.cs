using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightProbeProxyVolume;

public class Nivel2 : MonoBehaviour
{
    public GameMana nivel;
    GameObject puerta;
    GameObject plataformas;
    public Animator animator;
    public AudioClip audioAbrir;  
    public AudioClip audioCerrar;
    private AudioSource audioSource;
    
    void Start()
    {
        plataformas = GameObject.Find("Plataformas");
        puerta = GameObject.Find("Puerta2");

        plataformas.SetActive(false);

        audioSource = puerta.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = puerta.AddComponent<AudioSource>();
        }
    }

    public void ActivarPlataformas()
    {
        ReproducirSonido(audioAbrir);
        animator.Play("Puerta", -1, 0f);
        plataformas.SetActive(true);
        puerta.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.Play("PuertaCerrar");
            ReproducirSonido(audioCerrar);
            nivel.Nivel3();
            this.GetComponent<Collider>().enabled = false;
        }
    }

    private void ReproducirSonido(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}