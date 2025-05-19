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
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 posjugador = jugador.transform.position;
        Vector3 pospuerta = puerta.transform.position;

        float distancia = Vector3.Distance(posjugador, pospuerta);

        if (distancia > 15)
        {
            puerta.gameObject.SetActive(true);
        }
        else if (distancia < 15)
        {
            animator.Play(stateName: "Puerta");
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            animator.Play("PuertaCerrar");
            Destroy(gameObject);
        }
    }

}
