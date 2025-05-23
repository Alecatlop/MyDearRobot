using System.Collections;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemigoIA: MonoBehaviour
{
    Estado FSM;
    public GameObject jugador;
    public GameObject centro;
    public NavMeshAgent agent;
    GameObject rayotrigger;
    GameObject rayolaser;
    public GameObject[] platasformas;
    public Animator animator;
    public GameObject[] runas;

    public int contadorrunas;
    public int runarandom;
    public int[] posicionOcupada = new int[6];
    public int fase;
    float dist;
    public int vidas = 3;
    public bool ocupado;
    public bool luzruna;
    public bool puedeHacerSuperataque;
    public bool Superataqueactivo;

    void Start()
    {
      
        FSM = new Fase1();
        FSM.inicializarVariables(this);
        rayotrigger = GameObject.Find("RAYO");
        rayolaser = GameObject.Find("RAYOLASER");
        rayotrigger.SetActive(false);
        rayolaser.SetActive(false);

        for (int i = 0; i < posicionOcupada.Length; i++)
        {
            posicionOcupada[i] = -1;
        }

        transform.LookAt(jugador.transform.position);
    }

    void Update()
    {
        FSM = FSM.Procesar();

        if (runas[runarandom].GetComponent<Runas5>().runapintada == true && fase == 3)
        {
            posicionOcupada[runarandom] = 1;
        }
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

            if (probabilidad > 0 && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                StartCoroutine(Ataque3());
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                StartCoroutine(Ataque2());
                return true;
            }

            return true;
        }
        else if (dist < 12f && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false)
        {
            ocupado = true;
            StartCoroutine(Ataque1());
            return true;
        }
        else if (dist > 35f && vidas == 3 && ocupado == false)
        {
            ocupado = true;
            StartCoroutine(Ataque2());
            return true;
        }
        else return false;
    }

    IEnumerator Ataque1()
    {
        this.agent.speed = 0f;
        animator.SetBool("pisoton", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(3f);
        animator.SetBool("pisoton", false);

        animator.SetBool("caminar", true);
        agent.speed = 2f;
        agent.SetDestination(jugador.transform.position);

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    IEnumerator Ataque2()
    {
        this.agent.speed = 0f;
        animator.SetBool("puñetazo", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(4f);
        animator.SetBool("puñetazo", false);

        animator.SetBool("caminar", true);
        agent.speed = 2f;
        agent.SetDestination(jugador.transform.position);

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    IEnumerator Ataque3()
    {
        this.agent.speed = 0f;
        animator.SetBool("rayo", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(3f);
        animator.SetBool("rayo", false);
        animator.SetBool("rayo descanso", true);

        yield return new WaitForSeconds(1.5f);
        animator.SetBool("rayo descanso", false);

        animator.SetBool("caminar", true);
        agent.speed = 2f;
        agent.SetDestination(jugador.transform.position);

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    public void Golpearsuelo()
    {
        if (ocupado == false)
        {
            ocupado = true;
            animator.SetBool("terremoto", true);
            animator.SetBool("caminar", false);
        }
    }

    public void IniciarSuperataque()
    {
        StartCoroutine(Superataque());
    }

    IEnumerator Superataque()
    {
        ocupado = true;

        yield return new WaitForSeconds(2f);
        animator.SetBool("furia", true);

        yield return new WaitForSeconds(2f);
        animator.SetBool("furia", false);
        animator.SetBool("superataque", true);
        animator.SetBool("caminar", false);
        runarandom = Random.Range(0, 6);

        do
        {
            runarandom = Random.Range(0, 6);
        }
        while (posicionOcupada[runarandom] == 1);
        runas[runarandom].GetComponent<Runas5>().runaLista = true;


        yield return new WaitForSeconds(2f);
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverArriba();
        Superataqueactivo = true;
        puedeHacerSuperataque = false;
        this.agent.speed = 0f;

        StartCoroutine(CargaSuperataque());
    }

    public void ActivarRayo()
    {
        if (contadorrunas == 6 && fase < 3)
        {
            rayotrigger.SetActive(true);
            contadorrunas = 0;
        }
        else if (contadorrunas == 6 && fase == 3)
        {
            rayotrigger.SetActive(true);
        }
    }

    IEnumerator DisparandoRayo()
    {
        ocupado = true;
        rayolaser.SetActive(true);

        yield return new WaitForSeconds(2f);
        StartCoroutine(Dañado());
        rayolaser.SetActive(false);
    }

    IEnumerator Dañado()
    {
        if (vidas == 1)
        {
            animator.SetBool("dañado", true); vidas--; ocupado = true;
        }
        else
        {
            animator.SetBool("terremoto", false);
            animator.SetBool("dañado", true);
            animator.SetBool("caminar", false);
            ocupado = true;
            this.agent.speed = 0f;
        

            yield return new WaitForSeconds(2f);
            animator.SetBool("dañado", false);
            animator.SetBool("furia", true);


            yield return new WaitForSeconds(4f);
            animator.SetBool("furia", false);
            vidas--;
            luzruna = false;
            Debug.Log("Dañado, le quedan " + vidas);
            ocupado = false;
        }

    }

    public void Morir()
    {
        //SceneManager.LoadScene("Cinematica Final Malo");
        if (jugador.GetComponent<CharacterControllerScript>().muertesActuales >= 6)
        {
            SceneManager.LoadScene("Cinematica Final Malo");
        }
        else SceneManager.LoadScene("Cinematica Final Bueno");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RAYO")
        {
            StartCoroutine(DisparandoRayo());
            rayotrigger.SetActive(false);
        }
    }

    IEnumerator CargaSuperataque()
    {
        yield return new WaitForSeconds(10f);
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverAbajo();
        Superataqueactivo = false;
        animator.SetBool("superataque", false);

        yield return new WaitForSeconds(5f);
        ocupado = false;

        yield return new WaitForSeconds(20f);
        puedeHacerSuperataque = true;
    }

    IEnumerator CambiarFase()
    {
        yield return new WaitForSeconds(2f);

        if (vidas == 2)
        {
            Golpearsuelo();
        }

        fase++;
        ocupado = false;
    }

    public void lanzarCorrutinaFase()
    {
        StartCoroutine(CambiarFase());
    }

}


