using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    GameObject jugador;
    public GameObject puerta;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posjugador = jugador.transform.position;
        Vector3 pospuerta = puerta.transform.position;

        float distancia = Vector3.Distance(posjugador, pospuerta);

        if (distancia > 15)
        {
            //animator.enabled = true;
            puerta.gameObject.SetActive(true);
        }
        else if (distancia < 15)
        {
            puerta.gameObject.SetActive(false);
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            puerta.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

}
