using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    GameObject jugador;
    public GameObject puerta;
    public Animator animator;
    public AudioClip audioAbrir;
    public AudioClip audioCerrar;
    private AudioSource audioSource;
    private bool puertaAbierta = false;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        audioSource = puerta.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = puerta.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 posjugador = jugador.transform.position;
        Vector3 pospuerta = puerta.transform.position;

        float distancia = Vector3.Distance(posjugador, pospuerta);

        if (distancia > 15)
        {
            puerta.gameObject.SetActive(true);
            if (puertaAbierta)
            {
                animator.Play("PuertaCerrar");
                ReproducirSonido(audioCerrar);
                puertaAbierta = false;
            }
        }
        else if (distancia < 15)
        {
            animator.Play("Puerta");
            if (!puertaAbierta)
            {
                ReproducirSonido(audioAbrir);
                puertaAbierta = true;
            }
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            animator.Play("PuertaCerrar");
            ReproducirSonido(audioCerrar);
            Destroy(gameObject);
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