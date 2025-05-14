using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas1 : MonoBehaviour
{
    public EnemigoIA scriptenemigo;
    float speed = 3f;
    bool subido = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptenemigo.vidas == 2 && subido == false)
        {

            StartCoroutine(movimientoplataformas1());
        }
        else if (scriptenemigo.vidas == 1 && subido == true)
        {
            subido = false;
            StartCoroutine(movimientoplataformas1());
        }
    }


    IEnumerator movimientoplataformas1()
    {
        if (scriptenemigo.vidas == 2)
        {
            yield return new WaitForSeconds(4);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            yield return new WaitForSeconds(1);

            subido = true;

        }
        else if (scriptenemigo.vidas == 1)
        {
            yield return new WaitForSeconds(4);

            transform.Translate(Vector3.back * Time.deltaTime * speed);
            
        }


    }
}
