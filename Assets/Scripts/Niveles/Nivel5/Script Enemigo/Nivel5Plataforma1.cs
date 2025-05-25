using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Nivel5Plataformas1 : MonoBehaviour
{

    public EnemigoIA scriptenemigo;
    bool subido = false;
    float speed = 3f;
    private AudioSource audioSource;
    private bool subiendo = false;
    private bool bajando = false;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float volumenGlobal = PlayerPrefs.GetFloat("efectos", 1f);
        audioSource.volume = volumenGlobal * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //// mover con funcion que se accede desde enemigo
        //if (scriptenemigo.vidas == 2 && subido == false)
        //{
        //    StartCoroutine(SubirPlataformas());
        //}

        //if (scriptenemigo.vidas == 1 && subido == true)
        //{
        //    print("abajo");
        //    StartCoroutine(BajarPlataformas());
        //}

        if (Pausa.juegoPausado) return;

        float volumenGlobal = PlayerPrefs.GetFloat("efectos", 1f);
        audioSource.volume = volumenGlobal * 0.5f;

        if ((subiendo || bajando) && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!subiendo && !bajando && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Limite superior")
        {
            StopAllCoroutines();
            subido = true;
            subiendo = false;
        }

        if (other.name == "Limite inferior")
        {
            StopAllCoroutines();
            subido = false;
            bajando = false;
        }
    }

    public void MoverArriba()
    {
        StartCoroutine(SubirPlataformas());
    }

    public void MoverAbajo()
    {
        StartCoroutine(BajarPlataformas());
    }

    IEnumerator SubirPlataformas()
    {
        subiendo = true;
        bajando = false;

        // sube todas las plataformas runa
        while (subido == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;

        }

        subiendo = false;
    }

    IEnumerator BajarPlataformas()
    {
        bajando = true;
        subiendo = false;

        // sube todas las plataformas runa
        while (subido == true)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            yield return null;

        }

        bajando = false;
    }

  
}
