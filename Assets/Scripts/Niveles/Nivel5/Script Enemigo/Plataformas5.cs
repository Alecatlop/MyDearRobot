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
    float time;

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
        
        if (scriptenemigo.vidas == 1 && subido == true && desactivado == false)
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

            yield return new WaitForSeconds(1);
            subido = true;
        }
        StartCoroutine(MovimientoPlataformas2());
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

            yield return new WaitForSeconds(1f);

            subido = false;
            desactivado = true;
        }
    }

    IEnumerator MovimientoPlataformas2()
    {
        // sube y baja en ciclo todas las plataformas 2
        while (scriptenemigo.vidas == 2 && subido == true)
        {
            yield return new WaitForSeconds(5);

            if (subido2 == false)
            {
                for (int i = 0; i < plataformas2.Length; i++)
                {
                    plataformas2[i].transform.Translate(Vector3.forward * Time.deltaTime * 3.5f);
                }
                yield return new WaitForSeconds(2.8f);
                subido2 = true;
            }

            yield return new WaitForSeconds(5);

            if (subido2 == true)
            {
                for (int i = 0; i < plataformas2.Length; i++)
                {
                    plataformas2[i].transform.Translate(Vector3.back * Time.deltaTime * 3.5f);
                }

                yield return new WaitForSeconds(2.8f);
                subido2 = false;
            }
        }
    }

    IEnumerator MovimientoPrueba()
    {
        time = 0;

        while (time < 1)
        {
            time += Time.deltaTime;

            transform.position = Vector3.Lerp(new Vector3(transform.position.x, 21.01345f, transform.position.z), new Vector3(transform.position.x, 22.48f, transform.position.z), time);

            yield return new WaitForEndOfFrame();
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

}
