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
      
        FSM = new Fase1();

        //if(fase == 3)
        //{
        //    FSM = new Fase3();
        //}

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
                StartCoroutine(Ataque3());
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && Superatataqueactivo == false && puedeHacerSuperataque == false)
            {
                ocupado = true;
                StartCoroutine(Ataque2());
                return true;
            }

            return true;
        }
        else if (dist < 12f && ocupado == false && Superatataqueactivo == false && puedeHacerSuperataque == false)
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
        Debug.Log("Ataque1 / PISOTON");
        this.agent.speed = 0f;
        animator.SetBool("pisoton", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(3f);
        animator.SetBool("pisoton", false);

        yield return new WaitForSeconds(3f);
        ocupado = false;
    }

    IEnumerator Ataque2()
    {
        Debug.Log("Ataque2 / PUÑETAZO");
        this.agent.speed = 0f;
        animator.SetBool("puñetazo", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(4f);
        animator.SetBool("puñetazo", false);

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    IEnumerator Ataque3()
    {
        Debug.Log("Ataque3 / RAYO");
        this.agent.speed = 0f;
        animator.SetBool("rayo", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(3f);
        animator.SetBool("rayo", false);
        animator.SetBool("rayo descanso", true);

        yield return new WaitForSeconds(1.5f);
        animator.SetBool("rayo descanso", false);

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    public void Golpearsuelo()
    {
        if (ocupado == false)
        {
            print("Terremoto");
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
        print("inicio Superatataque");
        animator.SetBool("furia", false);
        animator.SetBool("superataque", true);
        animator.SetBool("caminar", false);
        runarandom = Random.Range(0, 6);

        // condicion si ha activado todas volver todas a -1 o dejar de hacer hacer superataque
        do
        {
            runarandom = Random.Range(0, 6);
        }
        while (posicionOcupada[runarandom] == 1);

        yield return new WaitForSeconds(2f);
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverArriba();
        posicionOcupada[runarandom] = 1;
        Superatataqueactivo = true;
        puedeHacerSuperataque = false;
        this.agent.speed = 0f;

        StartCoroutine(CargaSuperataque());
    }

    public void ActivarRayo()
    {
        if (contadorrunas == 6)
        {
            print("RayoTrigger activado");
            rayotrigger.SetActive(true);
            contadorrunas = 0;
        }
    }

    IEnumerator DisparandoRayo()
    {
        print("Disparar Rayo");
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
            luzruna = false;

            yield return new WaitForSeconds(2f);
            animator.SetBool("dañado", false);
            animator.SetBool("furia", true);


            yield return new WaitForSeconds(4f);
            animator.SetBool("furia", false);
            vidas--;
            Debug.Log("Dañado, le quedan " + vidas);
            ocupado = false;
        }

    }

    public void Morir()
    {
        print("morir");
        SceneManager.LoadScene("Cinematica Final Malo");
        //if (jugador.GetComponent<CharacterControllerScript>().muertesActuales >= 6)
        //{
        //    SceneManager.LoadScene("Cinematica Final Malo");
        //}
        //else SceneManager.LoadScene("Cinematica Final Bueno");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RAYO")
        {
            print("Rayo Lanzado");
            StartCoroutine(DisparandoRayo());
            rayotrigger.SetActive(false);
        }
    }

    IEnumerator CargaSuperataque()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("Cargando SUPERATAQUE");
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverAbajo();
        Superatataqueactivo = false;
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


