using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    int contadorrunas = -1;
    public GameMana nivel;
    public GameObject puerta;
    public GameObject puertasalida;
    public GameObject tierra;
    public GameObject arena;
    public Animator animator;
    public AudioClip audioAbrir;  
    public AudioClip audioCerrar;
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Modelo ruinas");
        puerta.GetComponent<Animator>().enabled = false;
        arena = GameObject.Find("Arena");
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }

    public void RunaColor()
    {
        if (contadorrunas < 2)
        {
            contadorrunas++;
        }

        if (contadorrunas == 2)
        {
            puerta.GetComponent<Animator>().enabled = true;
            animator.Play(stateName: "Animacion_Completa");

            StartCoroutine(SonidoAbrirRetrasado(3f));

            tierra.GetComponent<Collider>().enabled = true;
            tierra.SetActive(false);
        }
    }

    private IEnumerator SonidoAbrirRetrasado(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioAbrir != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;
            audioSource.PlayOneShot(audioAbrir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            arena.SetActive(false);
            puertasalida.SetActive(true);
            animator.Play(stateName: "PuertaCerrar");

            if (audioCerrar != null)
            {
                audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;
                audioSource.PlayOneShot(audioCerrar);
            }

            nivel.Nivel1();
            nivel.Nivel2();
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
