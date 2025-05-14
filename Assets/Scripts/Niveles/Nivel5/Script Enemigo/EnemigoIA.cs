using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;  

public class EnemigoIA: MonoBehaviour
{
    Estado FSM;
    public GameObject jugador;
    public GameObject centro;
    public NavMeshAgent agent;

    float dist;
    public int vidas = 3;
    public bool ocupado;
    public bool superataque;

    void Start()
    {
        FSM = new Fase1();
        FSM.inicializarVariables(this);

        StartCoroutine(Espera());
    }

    void Update()
    {
        FSM = FSM.Procesar();
        
        if (vidas == 1 && ocupado == false && superataque == true)
        {
            Superataque();
        }
    }

    public bool PuedeAtacar()
    {
        dist = Vector3.Distance(jugador.transform.position, transform.position);

        if (dist > 35f && vidas == 1 && ocupado == false)
        {
            int probabilidad = Random.Range(0, 3);

            if (probabilidad > 0 && ocupado == false && superataque == false)
            {
                ocupado = true;
                Ataque3();
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && superataque == false)
            {
                ocupado = true;
                Ataque2();
                return true;
            }

            return true;
        }
        else if (dist < 18f && ocupado == false && superataque == false)
        {
            ocupado = true;
            Ataque1();
            return true;
        }
        else if (dist > 35f && vidas == 3 && ocupado == false)
        {
            ocupado = true;
            Ataque2();
            return true;
        }
        else return false;
    }

    public void Ataque1()
    {
        Debug.Log("Ataque1 / PISOTON");
        this.agent.speed = 0f;
        StartCoroutine(Espera());
    }

    public void Ataque2()
    {
        Debug.Log("Ataque2 / PUÑETAZO");
        this.agent.speed = 0f;
        StartCoroutine(Espera());
    }

    public void Ataque3()
    {
        Debug.Log("Ataque3 / RAYO");
        this.agent.speed = 0f;
        StartCoroutine(Espera());
    }

    public void Golpearsuelo()
    {
        this.agent.SetDestination(centro.transform.position);
        this.agent.speed = 6f;
        StartCoroutine(Espera());

        if (ocupado == false)
        {
            Debug.Log("Golpearsuelo / TERREMOTO");
        }
    }

    public void Superataque()
    {
        ocupado = true;
        Debug.Log("Superataque");
        this.agent.speed = 0f;
        StartCoroutine(Espera2());
    }

    public void Dañado()
    {
        ocupado = true;
        vidas--;
        Debug.Log("Dañado, le quedan " + vidas);
        StartCoroutine(Espera());
    }

    public void Morir()
    {
        Debug.Log("Muerto");
        this.gameObject.SetActive(false);   
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name == "RAYO")
        {
            Dañado();
        }
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(6f);
        ocupado = false;
    }

    IEnumerator Espera2()
    {
        yield return new WaitForSeconds(12f);
        superataque = false;
        ocupado = false;

        yield return new WaitForSeconds(20f);
        superataque = true;
    }

}


