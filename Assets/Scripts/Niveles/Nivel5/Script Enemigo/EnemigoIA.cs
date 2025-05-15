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

    public int runa;
    int[] runas;
    float dist;
    public int vidas = 3;
    public bool ocupado;
    public bool puedeHacerSuperataque;

    void Start()
    {
        FSM = new Fase1();
        FSM.inicializarVariables(this);

        StartCoroutine(Espera());
        //runas[0] = 1; runas[1] = 2; runas[2] = 3; runas[3] = 4; runas[4] = 5; runas[5] = 6; 
    }

    void Update()
    {
        FSM = FSM.Procesar();


    }

    public void TerminarCorrutinas()
    { 
        StopAllCoroutines();
    }

    public bool PuedeAtacar()
    {
        dist = Vector3.Distance(jugador.transform.position, transform.position);

        if (dist > 35f && vidas == 1 && ocupado == false)
        {
            int probabilidad = Random.Range(0, 3);

            if (probabilidad > 0 && ocupado == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                Ataque3();
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                Ataque2();
                return true;
            }

            return true;
        }
        else if (dist < 18f && ocupado == false && puedeHacerSuperataque == false)
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

        if (ocupado == false)
        {
            Debug.Log("Golpearsuelo / TERREMOTO");
            StartCoroutine(Espera());
        }
    }

    public void Superataque()
    {
        //do
        //{
        //    runa = Random.Range(0, 6);

        //    for (int i = 0; i < runas.Length; i++)
        //    {
        //        i = runa;
        //        runas[i] == runa;
        //    }
        //}
        //while runas[i] == runa;

        Debug.Log("Superataque s=true");
        ocupado = true;
        this.agent.speed = 0f;
        StartCoroutine(Espera2());
    }

    public void Dañado()
    {
        Debug.Log("DAÑADO");
        ocupado = true;
        vidas--;
        Debug.Log("Dañado, le quedan " + vidas);
        StartCoroutine(Espera());
    }

    public void lanzarCorrutinaFase3()
    {
        StartCoroutine(EntrarFase3());
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
        puedeHacerSuperataque = false;
        ocupado = false;

        yield return new WaitForSeconds(20f);
        puedeHacerSuperataque = true;
    }

    public IEnumerator EntrarFase3()
    {
        yield return new WaitForSeconds(5f);
        ocupado = false;
    }


}


