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

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptenemigo.vidas == 2 && subido == false)
        {
            StartCoroutine(SubirPlataformas());
        }
        
        if (scriptenemigo.vidas == 1 && subido == true)
        {
            StartCoroutine(BajarPlataformas());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Limite superior")
        {
            StopAllCoroutines();
            subido = true;
            StartCoroutine(Espera());
        }

        if (other.name == "Limite inferior")
        {
            StopAllCoroutines();
            subido = false;
            StartCoroutine(Espera());
        }
    }

    IEnumerator SubirPlataformas()
    {
        // sube todas las plataformas runa
        if (scriptenemigo.vidas == 2 && subido == false)
        {
            yield return new WaitForSeconds(2);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
          
        }
    }

    IEnumerator BajarPlataformas()
    {
        // sube todas las plataformas runa
        if (scriptenemigo.vidas == 1 && subido == true)
        {
            yield return new WaitForSeconds(2);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);

        }
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(5);
        subido = !subido;
    }

}
