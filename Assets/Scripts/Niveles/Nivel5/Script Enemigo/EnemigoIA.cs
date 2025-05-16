using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA: MonoBehaviour
{
    Estado FSM;
    public GameObject jugador;
    public GameObject centro;
    public NavMeshAgent agent;
    GameObject rayo;

    public int contadorrunas;
    public int runarandom;
    public int fase;
    float dist;
    public int vidas = 3;
    public bool ocupado;
    public bool luzruna;
    public bool puedeHacerSuperataque;

    void Start()
    {
        FSM = new Fase1();
        FSM.inicializarVariables(this);
        rayo = GameObject.Find("RAYO");
        rayo.SetActive(false);
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
        StartCoroutine(Cargaataque());
    }

    public void Ataque2()
    {
        Debug.Log("Ataque2 / PUÑETAZO");
        this.agent.speed = 0f;
        StartCoroutine(Cargaataque());
    }

    public void Ataque3()
    {
        Debug.Log("Ataque3 / RAYO");
        this.agent.speed = 0f;
        StartCoroutine(Cargaataque());
    }

    public void Golpearsuelo()
    {
        if (ocupado == false)
        {
            ocupado = true;
            Debug.Log("Golpearsuelo / TERREMOTO");
        }
    }

    public void Superataque()
    {
        int runarandom = Random.Range(0, 6);
        
        Debug.Log("Superataque");
        ocupado = true;
        this.agent.speed = 0f;
        StartCoroutine(CargaSuperataque());
    }

    public void ActivarRayo()
    {
        if (contadorrunas == 6)
        {
            rayo.SetActive(true);
            contadorrunas = 0;
           
        }
    }

    public void FASE()
    {
        StartCoroutine(CambiarFase());
    }

    IEnumerator Dañado()
    {
        ocupado = true;
        this.agent.speed = 0f;
        vidas--;
        luzruna = false;
       
        Debug.Log("Dañado, le quedan " + vidas);
        ocupado = false;
        yield return null;
    }

      IEnumerator CambiarFase()
    {
        yield return new WaitForSeconds(5f);
        print("cambiar fase");
        fase++;
    }

    public void lanzarCorrutinaFase3()
    {
        StartCoroutine(CambioFase3());
    }

    public void lanzarCorrutinaFase()
    {
        StartCoroutine(CambioFase());
    }

    public void Morir()
    {
        Debug.Log("Muerto");
        this.gameObject.SetActive(false);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RAYO")
        {
            rayo.SetActive(false);
            StartCoroutine(Dañado());
        }
    }

    IEnumerator Cargaataque()
    {
        yield return new WaitForSeconds(6f);
        ocupado = false;
    }

    IEnumerator CargaSuperataque()
    {
      
        yield return new WaitForSeconds(12f);
        puedeHacerSuperataque = false;
        ocupado = false;

        Debug.Log("Cargando SUPERATAQUE");
        yield return new WaitForSeconds(20f);
        puedeHacerSuperataque = true;
    }

    IEnumerator CambioFase()
    {
        print("CAMBIO FASE");
        yield return new WaitForSeconds(5f);
        ocupado = false;
    }

    IEnumerator CambioFase3()
    {
        yield return new WaitForSeconds(10f);
        ocupado = false;
    }

}


