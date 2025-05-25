using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Nivel5Plataformas2 : MonoBehaviour
{

    public EnemigoIA scriptenemigo;
    bool subir = true;
    bool bajar = false;
    float speed = 6f;
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

        if (scriptenemigo.vidas == 2 && subir && !bajar)
        {
            StartCoroutine(SubirPlataformas());
        }

        if (scriptenemigo.vidas == 2 && !subir && bajar)
        {
            StartCoroutine(BajarPlataformas());
        }

        if (scriptenemigo.vidas < 2 && (subir || bajar))
        {
            subir = false;
            bajar = true;
            StartCoroutine(BajarPlataformas());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Limite superior 2")
        {
            if (scriptenemigo.vidas < 2)
            {
                print("Baja trigger");
                StopAllCoroutines();
                subir = false;
                bajar = true;
                StartCoroutine(BajarPlataformas());
            }
            else
            {
                StopAllCoroutines();
                subir = false;
                StartCoroutine(Espera());
            }
            
            subiendo = false;
        }

        if (other.name == "Limite inferior 2")
        {
            if (scriptenemigo.vidas < 2)
            {
                print("Para trigger");
                StopAllCoroutines();
                subir = false;
                bajar = false;
            }
            else
            {
                StopAllCoroutines();
                bajar = false;
                StartCoroutine(Espera2());
            }

            bajando = false;
        }
    }

    IEnumerator SubirPlataformas()
    {
        subiendo = true;
        bajando = false;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        yield return null;
    }

    IEnumerator BajarPlataformas()
    {
        bajando = true;
        subiendo = false;

        transform.Translate(Vector3.back * Time.deltaTime * speed);
        yield return null;
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(6);
        bajar = true;
    }

    IEnumerator Espera2()
    {
        yield return new WaitForSeconds(2);
        subir = true;
    }
}
