using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Plataforma_1 : MonoBehaviour
{
    bool avanzar = false;
    bool retroceder = false;
    public float speed;
    public Transform centroCupula; 

    private Vector3 direccion;

    void Start()
    {
        if (centroCupula != null)
        {
            // Calcula la dirección hacia el centro en el plano XZ
            Vector3 centroEnMismoNivel = new Vector3(centroCupula.position.x, transform.position.y, centroCupula.position.z);
            direccion = (centroEnMismoNivel - transform.position).normalized;

            // Orienta la plataforma mirando hacia el centro
            transform.rotation = Quaternion.LookRotation(direccion);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el centro de la cúpula en la plataforma " + gameObject.name);
        }
    }

    void OnEnable()
    {
        StartCoroutine(Movimiento());
    }

    void Update()
    {
        if (avanzar)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (retroceder)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
    }

    private IEnumerator Movimiento()
    {
        while (true) 
        {
            retroceder = true;
            yield return new WaitForSeconds(2.5f);

            retroceder = false;
            yield return new WaitForSeconds(2f);

            avanzar = true;
            yield return new WaitForSeconds(2.5f);

            avanzar = false;
            yield return new WaitForSeconds(2f);
        }
    }
}
