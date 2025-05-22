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
    public GameObject[] platasformas;
    public Animator animator;

    public int contadorrunas;
    public int runarandom;
    int[] posicionOcupada = new int[6];
    public int fase;
    float dist;
    public int vidas = 3;
    public bool ocupado;
    public bool luzruna;
    public bool puedeHacerSuperataque;
    public bool Superatataqueactivo;

    void Start()
    {
      

        if (fase == 1)
        {
            FSM = new Fase1();
        }
        else if(fase == 3)
        {
            FSM = new Fase3();
        }

        FSM = new Fase1();
        FSM.inicializarVariables(this);
        rayo = GameObject.Find("RAYO");
        rayo.SetActive(false);

        for (int i = 0; i < posicionOcupada.Length; i++)
        {
            posicionOcupada[i] = -1;
        }

        transform.LookAt(jugador.transform.position);
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

            if (probabilidad > 0 && ocupado == false && Superatataqueactivo == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                Ataque3();
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && Superatataqueactivo == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                Ataque2();
                return true;
            }

            return true;
        }
        else if (dist < 12f && ocupado == false && Superatataqueactivo == false && puedeHacerSuperataque == false)
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
        animator.SetBool("pisoton", true);
        animator.SetBool("caminar", false);
        StartCoroutine(Cargaataque());
    }

    public void Ataque2()
    {
        Debug.Log("Ataque2 / PUÑETAZO");
        this.agent.speed = 0f;
        animator.SetBool("puñetazo", true);
        animator.SetBool("caminar", false);
        StartCoroutine(Cargaataque());
    }

    public void Ataque3()
    {
        Debug.Log("Ataque3 / RAYO");
        this.agent.speed = 0f;
        animator.SetBool("rayo", true);
        animator.SetBool("caminar", false);
        StartCoroutine(Cargaataque());
    }

    public void Golpearsuelo()
    {
        if (ocupado == false)
        {
            ocupado = true;
            animator.SetBool("terremoto", true);
            animator.SetBool("caminar", false);
            Debug.Log("Golpearsuelo / TERREMOTO");
        }
    }

    public void Superataque()
    {
        animator.SetBool("superataque", true);
        animator.SetBool("caminar", false);
        Debug.Log("Cuantas VECES SE EJECUTA SUPERATAQUE");
        runarandom = Random.Range(0, 6);

        // condicion si ha activado todas volver todas a -1 o dejar de hacer hacer superataque
        do
        {
            runarandom = Random.Range(0, 6);
        }
        while (posicionOcupada[runarandom] == 1);

        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverArriba();
        posicionOcupada[runarandom] = 1;

        ocupado = true;
        Superatataqueactivo = true;
        puedeHacerSuperataque = false;
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

    IEnumerator Dañado()
    {
        animator.SetBool("dañado", true);
        animator.SetBool("caminar", false);
        ocupado = true;
        this.agent.speed = 0f;
        vidas--;
        luzruna = false;

        yield return new WaitForSeconds(2f);
        Debug.Log("Dañado, le quedan " + vidas);
        ocupado = false;
        yield return null;
        animator.SetBool("dañado", false);
    }

    public void Morir()
    {
        animator.SetBool("caminar", false);
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
        yield return new WaitForSeconds(5f);
        animator.SetBool("pisoton", false);
        animator.SetBool("puñetazo", false);
        animator.SetBool("rayo", false);
        ocupado = false;
    }

    IEnumerator CargaSuperataque()
    {
        yield return new WaitForSeconds(12f);
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverAbajo();
        Superatataqueactivo = false;

        yield return new WaitForSeconds(5f);
        animator.SetBool("superataque", false);
        ocupado = false;


        Debug.Log("Cargando SUPERATAQUE");
        yield return new WaitForSeconds(20f);
        puedeHacerSuperataque = true;
    }

    IEnumerator CambioFase3()
    {
        yield return new WaitForSeconds(10f);
        ocupado = false;
    }

    IEnumerator CambiarFase()
    {
        yield return new WaitForSeconds(4f);

        if (vidas == 2)
        {
            Golpearsuelo();
        }

        fase++;
    }

    public void lanzarCorrutinaFase3()
    {
        StartCoroutine(CambioFase3());
    }

    public void lanzarCorrutinaFase()
    {
        StartCoroutine(CambiarFase());
    }

}


