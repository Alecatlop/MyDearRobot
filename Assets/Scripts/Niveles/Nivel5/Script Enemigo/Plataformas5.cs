using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Plataformas5 : MonoBehaviour
{

    GameObject[] plataformas1;
    GameObject[] plataformas2;
    public EnemigoIA scriptenemigo;
    bool subido = false;
    bool subido2 = false;

    // Start is called before the first frame update
    void Start()
    {
        plataformas1 = GameObject.FindGameObjectsWithTag("plataformas1");
        plataformas2 = GameObject.FindGameObjectsWithTag("plataformas2");
    }

    // Update is called once per frame
    void Update()
    {
        print(subido);
        if (scriptenemigo.vidas == 2 && subido == false)
        {

            StartCoroutine(movimientoplataformas());
        }
        else if (scriptenemigo.vidas == 1 && subido == true)
        {
            StartCoroutine(movimientoplataformas());
        }
    }

    IEnumerator movimientoplataformas()
    {
        // sube todas las plataformas runa
        if (scriptenemigo.vidas == 2)
        {
            yield return new WaitForSeconds(4);

            for (int i = 0; i < plataformas1.Length; i++)
            {
                plataformas1[i].transform.Translate(Vector3.forward * Time.deltaTime * 2);
            }

            yield return new WaitForSeconds(1);

            subido = true;
        }
        else if (scriptenemigo.vidas == 1)
        {
            yield return new WaitForSeconds(4);

            for (int i = 0; i < plataformas1.Length; i++)
            {
                plataformas1[i].transform.Translate(Vector3.back * Time.deltaTime * 2);
            }

            yield return new WaitForSeconds(1);

            subido = false;
        }

        // sube plaatforma en secuencia con runa
        if (scriptenemigo.superataque == true && scriptenemigo.vidas == 1)
        {
            yield return new WaitForSeconds(11);

            plataformas1[scriptenemigo.runa].transform.Translate(Vector3.forward * Time.deltaTime * 2);

            yield return new WaitForSeconds(1);

            subido = true;
        }
        else if (scriptenemigo.superataque == false && scriptenemigo.vidas == 1)
        {
            plataformas1[scriptenemigo.runa].transform.Translate(Vector3.back * Time.deltaTime * 2);

            yield return new WaitForSeconds(1);

            subido = false;
        }

        // sube y baja en ciclo todas las plataformas 2
        while (scriptenemigo.vidas == 2 && subido == true)
        {
            if (subido2 == false)
            {
                for (int i = 0; i < plataformas2.Length; i++)
                {
                    plataformas2[i].transform.Translate(Vector3.forward * Time.deltaTime * 4f);
                }
                yield return new WaitForSeconds(2);
                subido2 = true;
            }

            yield return new WaitForSeconds(3);

            if (subido2 == true)
            {
                for (int i = 0; i < plataformas2.Length; i++)
                {
                    plataformas2[i].transform.Translate(Vector3.back * Time.deltaTime * 4f);
                }

                yield return new WaitForSeconds(2);
                subido2 = false;
            }

            yield return new WaitForSeconds(3);

        }

    }
}
