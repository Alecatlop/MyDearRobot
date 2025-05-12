using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;  // Added since we're using a navmesh.

public class EnemigoIA: MonoBehaviour
{
    Estado FSM;
    public GameObject jugador;
    public GameObject[] destinos;
    public NavMeshAgent agent;
    public GameObject rayo;
    GameObject instancia;
    public GameObject canon;
    public int a;
    public bool disparando;
    float dist;

    float fuerza = 30f;
    public GameObject posFinal;
    public Ray ray;
    

    void Start()
    {
        FSM = new Fase1();
        FSM.inicializarVariables(this);
    }

    void Update()
    {
       
        FSM = FSM.Procesar(); // INICIAMOS LA FSM
    }

    public bool PuedeMover()
    {
        Vector3 posIA = this.transform.position;
        Vector3 pos = destinos[a].transform.position;

        float distancia = Vector3.Distance(posIA, pos);

        if (distancia <= 1f)
        {
            return true;
        }
        else return false; // DE MOMENTO NO
    }

    IEnumerator recarga()
    {
       
        while (true)
        {
            this.agent.speed = 0f;
            this.agent.SetDestination(jugador.transform.position);
            
            instancia = Instantiate(rayo, canon.transform.position, Quaternion.identity);
            instancia.transform.LookAt(jugador.transform.position);
            instancia.transform.Rotate(0,90, 90);

            instancia.GetComponent<Rigidbody>().AddForce(transform.forward * fuerza, ForceMode.Impulse);
            yield return new WaitForSeconds(2f);
        }
       
    }

    public void Disparar()
    {
       
    }

    public bool PuedeGolpear()
    {
        dist = Vector3.Distance(jugador.transform.position, transform.position);

        if (dist < 2.5f)
        {
            Ataque1();
        }

        return false;
    }

    public void Ataque1()
    {
        Debug.Log("Pisotón");
    }

    public void Golpearsuelo()
    {
        Debug.Log("Golpearsuelo");
    }


}

