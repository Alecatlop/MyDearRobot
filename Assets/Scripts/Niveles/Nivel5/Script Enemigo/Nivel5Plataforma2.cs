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
    float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptenemigo.vidas == 2 && subir == true && bajar == false)
        {
            StartCoroutine(SubirPlataformas());
        }
        
        if (scriptenemigo.vidas == 2 && subir == false && bajar == true)
        {
            StartCoroutine(BajarPlataformas());
        }

        if (scriptenemigo.vidas < 2 && (subir == true || bajar == true))
        {
            subir = false;
            bajar = true;
            StartCoroutine(BajarPlataformas());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Limite superior")
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
        }
    }

    IEnumerator SubirPlataformas()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        yield return null;
    }

    IEnumerator BajarPlataformas()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
        yield return null;
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5);
        bajar = true;
    }

    IEnumerator Espera2()
    {
        yield return new WaitForSeconds(5);
        subir = true;
    }

}
