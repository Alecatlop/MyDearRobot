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
            StartCoroutine(MovimientoPlataformas());
        }
        else if (scriptenemigo.vidas == 1 && subido == true && scriptenemigo.fase == 2)
        {
            StartCoroutine(MovimientoPlataformas());
        }

        if (scriptenemigo.vidas == 1 && scriptenemigo.puedeHacerSuperataque == true && scriptenemigo.fase == 3)
        {
            StartCoroutine(MovimientoPlataformaFase3());
        }
    }

    IEnumerator MovimientoPlataformas()
    {
        // sube todas las plataformas runa
        if (scriptenemigo.vidas == 2 && subido == false)
        {
            yield return new WaitForSeconds(4);

            for (int i = 0; i < plataformas1.Length; i++)
            {
                plataformas1[i].transform.Translate(Vector3.forward * Time.deltaTime * 2);
            }

            //yield return new WaitForSeconds(1);
            subido = true;
        }
        else if (scriptenemigo.vidas == 1)
        {
            yield return new WaitForSeconds(4);

            for (int i = 0; i < plataformas1.Length; i++)
            {
                plataformas1[i].transform.Translate(Vector3.back * Time.deltaTime * 2);
            }

            //yield return new WaitForSeconds(1f);

            subido = false;
        }

        StartCoroutine(MovimientoPlataformas2());
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
            print("muevete gordooooooo");
            time += Time.deltaTime;

            transform.position = Vector3.Lerp(new Vector3(transform.position.x, 21.01345f, transform.position.z), new Vector3(transform.position.x, 22.48f, transform.position.z), time);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MovimientoPlataformaFase3()
    {
        // sube plaatforma en secuencia con runa
        //if (scriptenemigo.puedeHacerSuperataque == true && scriptenemigo.vidas == 1)
        //{
        //    yield return new WaitForSeconds(11);

        //    plataformas1[scriptenemigo.contadorrunas].transform.Translate(Vector3.forward * Time.deltaTime * 2);

        //    yield return new WaitForSeconds(1);

        //    subido = true;
        //}
        //else if (scriptenemigo.puedeHacerSuperataque == false && scriptenemigo.vidas == 1)
        //{
        //    plataformas1[scriptenemigo.contadorrunas].transform.Translate(Vector3.back * Time.deltaTime * 2);

        //    yield return new WaitForSeconds(1);

        //    subido = false;
        //}

        if (scriptenemigo.puedeHacerSuperataque == true && subido == false)
        {
            yield return new WaitForSeconds(3);

            plataformas1[scriptenemigo.runarandom].transform.Translate(Vector3.forward * Time.deltaTime * 3);
            

            yield return new WaitForSeconds(1);
            subido = true;
        }

        yield return new WaitForSeconds(3);

        if (scriptenemigo.puedeHacerSuperataque == false && subido == true)
        {
            plataformas1[scriptenemigo.runarandom].transform.Translate(Vector3.back * Time.deltaTime * 3);

            yield return new WaitForSeconds(1);

            subido = false;
        }
    }

}
