using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Plataformas5 : MonoBehaviour
{

    GameObject[] plataformas1;
    GameObject[] plataformas2;
    public EnemigoIA scriptenemigo;
    bool subido = false;
    bool subido2 = false;
    bool desactivado = false;
    bool activo2 = false;

    // Start is called before the first frame update
    void Start()
    {
        plataformas1 = GameObject.FindGameObjectsWithTag("plataformas1");
        plataformas2 = GameObject.FindGameObjectsWithTag("plataformas2");
        //StartCoroutine(MovimientoPrueba());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (scriptenemigo.vidas == 2 && subido == false)
        {
            StartCoroutine(SubirPlataformas());
        }
        
        if (scriptenemigo.vidas == 1 && subido == true )//&& desactivado == false)
        {
            StartCoroutine(BajarPlataformas());
        }

        if (scriptenemigo.vidas == 1 && scriptenemigo.fase == 3 && scriptenemigo.Superatataqueactivo == true && subido == false)
        { 
            StartCoroutine(SubirPlataformaFase3());
        }

        if (scriptenemigo.vidas == 1 && scriptenemigo.fase == 3 && scriptenemigo.Superatataqueactivo == false && subido == true)
        {
            StartCoroutine(BajarPlataformaFase3());
        }

        if (scriptenemigo.vidas == 2 && activo2 == false)
        {
            StartCoroutine(MovimientoPlataformas2());
        }

        if (scriptenemigo.vidas < 2)
        {
            activo2 = false;
        }
    }

    IEnumerator SubirPlataformas()
    {
        // sube todas las plataformas runa
        if (scriptenemigo.vidas == 2 && subido == false)
        {
            yield return new WaitForSeconds(2);

            for (int i = 0; i < plataformas1.Length; i++)
            {
                plataformas1[i].transform.Translate(Vector3.forward * Time.deltaTime * 3);
            }

            //yield return new WaitForSeconds(1);
            //subido = true;
        }
        
    }

    IEnumerator BajarPlataformas()
    {
        if (scriptenemigo.vidas == 1 && subido == true)
        {
            yield return new WaitForSeconds(2);

            for (int i = 0; i < plataformas1.Length; i++)
            {
                plataformas1[i].transform.Translate(Vector3.back * Time.deltaTime * 3);
            }

            //yield return new WaitForSeconds(1f);

            //subido = false;
            //desactivado = true;
        }
    }

    IEnumerator MovimientoPlataformas2()
    {
        // sube y baja en ciclo todas las plataformas 2
            yield return new WaitForSeconds(2);

            if (subido2 == false)
            {
                for (int i = 0; i < plataformas2.Length; i++)
                {
                    plataformas2[i].transform.Translate(Vector3.forward * Time.deltaTime * 3.5f);
                }
            //yield return new WaitForSeconds(2.8f);
            //subido2 = true;

            }

        // yield return new WaitForSeconds(5);

        if (subido2 == true)
        {
            for (int i = 0; i < plataformas2.Length; i++)
            {
                plataformas2[i].transform.Translate(Vector3.back * Time.deltaTime * 3.5f);
            }

            //yield return new WaitForSeconds(2.8f);
            //subido2 = false;
        }
    }


    IEnumerator SubirPlataformaFase3()
    {
        if (subido == false)
        {
            yield return new WaitForSeconds(3);

            plataformas1[scriptenemigo.runarandom].transform.Translate(Vector3.forward * Time.deltaTime * 2);
        }

        subido = true;

    }

    IEnumerator BajarPlataformaFase3()
    {

        if (subido == true)
        {
            
            yield return new WaitForSeconds(3);

            plataformas1[scriptenemigo.runarandom].transform.Translate(Vector3.back * Time.deltaTime * 3);

        }

        subido = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "plataformas1")
        {
            StopAllCoroutines();
            subido = !subido;
            //subido = true;
        }

        if (other.tag == "plataformas2")
        {
            activo2 = true;
            StopAllCoroutines();

            if (activo2 == true)
            {
                if (subido2 == false)
                {
                    StartCoroutine(Espera());
                }
                else if (subido2 == true)
                {
                    StartCoroutine(Espera2());
                }
            }
        }

    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5);
        subido2 = true;
    }

    IEnumerator Espera2()
    {
        yield return new WaitForSeconds(5);
        subido2 = false;
    }

}
