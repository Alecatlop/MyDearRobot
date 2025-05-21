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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Limite superior")
        {
            StopAllCoroutines();
            subido = true;
        }

        if (other.name == "Limite inferior")
        {
            StopAllCoroutines();
            subido = false;
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
        // sube todas las plataformas runa
        while (subido == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;

        }
    }

    IEnumerator BajarPlataformas()
    {
        // sube todas las plataformas runa
        while (subido == true)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            yield return null;

        }
    }

  
}
