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
    public GameObject[] platasformas;
    public Animator animator;
    public GameObject[] runas;
    float velocidad = 2f;
    public GameObject[] avisorunas;

    GameObject COpisoton;
    GameObject particulaspisoton;
    GameObject COterremoto;
    GameObject particulasterremoto;
    GameObject COpuñetazo;
    GameObject particulaspuñetazo;
    Vector3 collidersize1;
    GameObject COsuperataque;
    GameObject particulasuperataque;
    Vector3 collidersize2;

    GameObject orbelaser;
    GameObject rayotrigger;
    GameObject rayolaser;
    GameObject rayolaserparticulas;
    public Animator animatorrayo;

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
    bool puño = false;
    bool superataque = false;

    void Start()
    {
      
        FSM = new Fase1();
        FSM.inicializarVariables(this);
        rayotrigger = GameObject.Find("corayo");
        rayolaser = GameObject.Find("RAYOLASER");
        rayolaserparticulas = GameObject.Find("rayolaser particulas");
        orbelaser = GameObject.Find("ORBELASER");
        rayotrigger.SetActive(false);
        rayolaser.SetActive(false);
        rayolaserparticulas.SetActive(false);
        orbelaser.SetActive(false);

        COpisoton = GameObject.Find("collider pisoton");
        COpisoton.SetActive(false);
        particulaspisoton = GameObject.Find("particulas pisoton");
        particulaspisoton.SetActive(false);
        COterremoto = GameObject.Find("collider terremoto");
        COterremoto.SetActive(false);
        particulasterremoto = GameObject.Find("terremoto");
        particulasterremoto.SetActive(false);
        COpuñetazo = GameObject.Find("copuñetazo");
        COpuñetazo.SetActive(false);
        particulaspuñetazo = GameObject.Find("puñetazo");
        particulaspuñetazo.SetActive(false);
        COsuperataque = GameObject.Find("COsuperataque");
        COsuperataque.SetActive(false);
        particulasuperataque = GameObject.Find("superataque");
        particulasuperataque.SetActive(false);

        collidersize1 = COpuñetazo.GetComponent<BoxCollider>().size;
        collidersize2 = COsuperataque.GetComponent<BoxCollider>().size;

        for (int i = 0; i < posicionOcupada.Length; i++)
        {
            posicionOcupada[i] = -1;
        }

        for (int i = 0; i < avisorunas.Length; i++)
        {
            avisorunas[i].SetActive(false);
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

        if (jugador.GetComponent<CharacterControllerScript>().daño == true && fase != 2)
        {
            StartCoroutine(EsperaJugador());
        }

        if (ocupado == true && puño == true)
        {
            COpuñetazo.GetComponent<BoxCollider>().size = new Vector3(COpuñetazo.GetComponent<BoxCollider>().size.x + 0.9f, COpuñetazo.GetComponent<BoxCollider>().size.y, COpuñetazo.GetComponent<BoxCollider>().size.z + 0.9f);
        }

        //if (ocupado == true && superataque == true)
        //{
        //    COsuperataque.GetComponent<BoxCollider>().size = new Vector3(COsuperataque.GetComponent<BoxCollider>().size.x + 0.9f, COsuperataque.GetComponent<BoxCollider>().size.y, COsuperataque.GetComponent<BoxCollider>().size.z + 0.9f);
        //}
    }

    public void TerminarCorrutinas()
    { 
        StopAllCoroutines();
    }

    public bool PuedeAtacar()
    {
        dist = Vector3.Distance(jugador.transform.position, transform.position);

       
        if (dist > 35f && vidas == 1 && ocupado == false && jugador.GetComponent<CharacterControllerScript>().daño == false)
        {
            int probabilidad = Random.Range(0, 3);

            if (probabilidad > 0 && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false && jugador.GetComponent<CharacterControllerScript>().daño == false)
            {
                ocupado = true;
                StartCoroutine(Ataque3());
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false && jugador.GetComponent<CharacterControllerScript>().daño == false)
            {
                ocupado = true;
                StartCoroutine(Ataque2());
                return true;
            }

            return true;
        }
        else if (dist < 15f && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false && jugador.GetComponent<CharacterControllerScript>().daño == false)
        {
            ocupado = true;
            StartCoroutine(Ataque1());
            return true;
        }
        else if (dist > 35f && vidas == 3 && ocupado == false && jugador.GetComponent<CharacterControllerScript>().daño == false)
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
       

        yield return new WaitForSeconds(1.8f);
        particulaspisoton.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        if (jugador.GetComponent<CharacterControllerScript>().daño == false)
        {
            COpisoton.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        COpisoton.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        particulaspisoton.SetActive(false);

        animator.SetBool("pisoton", false);
        SeguirJugador();

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    IEnumerator Ataque2()
    {
        this.agent.speed = 0f;
        animator.SetBool("puñetazo", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(2.2f);
        particulaspuñetazo.SetActive(true);
        COpuñetazo.SetActive(true);
        puño = true;

        yield return new WaitForSeconds(1f);
        puño = false;
        COpuñetazo.SetActive(false);
        COpuñetazo.GetComponent<BoxCollider>().size = collidersize1;

        yield return new WaitForSeconds(0.8f);
        animator.SetBool("puñetazo", false);
        particulaspuñetazo.SetActive(false);
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
        agent.speed = velocidad;
        agent.SetDestination(jugador.transform.position);

        yield return new WaitForSeconds(2f);
        ocupado = false;
    }

    IEnumerator Terremoto()
    {
        ocupado = true;
        agent.speed = 0;
        animator.SetBool("terremoto", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(5f);
        COterremoto.SetActive(true);
        particulasterremoto.SetActive(true);
    }

    public void IniciarSuperataque()
    {
        StartCoroutine(Superataque());
    }

    IEnumerator Superataque()
    {
        ocupado = true;
        this.agent.speed = 0f;
        runarandom = Random.Range(0, 6);
        do
        {
            runarandom = Random.Range(0, 6);
        }
        while (posicionOcupada[runarandom] == 1);
        runas[runarandom].GetComponent<Runas5>().runaLista = true;
        runas[runarandom].GetComponent<Runas5>().AvisoRuna();
        avisorunas[runarandom].SetActive(true);

        yield return new WaitForSeconds(2f);
        animator.SetBool("furia", true);

        yield return new WaitForSeconds(2f);
        animator.SetBool("furia", false);
        animator.SetBool("superataque", true);
        animator.SetBool("caminar", false);
        //superataque = true;
        //COsuperataque.SetActive(true);
        //particulasuperataque.SetActive(true);

        yield return new WaitForSeconds(5f);
        //superataque = false;
        //COsuperataque.SetActive(false);
        //particulasuperataque.SetActive(false);
        avisorunas[runarandom].SetActive(false);
        runas[runarandom].GetComponent<Runas5>().NoAvisoRuna();
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
            orbelaser.SetActive(true);
            rayotrigger.SetActive(true);
            contadorrunas = 0;
        }
        else if (contadorrunas == 6 && fase == 3)
        {
            orbelaser.SetActive(true);
            rayotrigger.SetActive(true);
        }
    }

    IEnumerator DisparandoRayo()
    {
        ocupado = true;
        rayolaser.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        rayolaserparticulas.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rayolaser.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Dañado());
        orbelaser.SetActive(false);
        rayolaserparticulas.SetActive(false);
    }

    IEnumerator Dañado()
    {
        if (vidas == 1)
        {
            animator.SetBool("dañado", true); vidas--; ocupado = true;
        }
        else
        {
            COterremoto.SetActive(false);
            particulasterremoto.SetActive(false);
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
        if (jugador.GetComponent<CharacterControllerScript>().muertesActuales >= 6)
        {
            SceneManager.LoadScene("Cinematica Final Malo");
        }
        else SceneManager.LoadScene("Cinematica Final Bueno");

    }

    IEnumerator EsperaJugador()
    {
        agent.speed = 0;
        ocupado = true;

        yield return new WaitForSeconds(3f);
        jugador.GetComponent<CharacterControllerScript>().daño = false;
        ocupado = false;
        SeguirJugador();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "corayo")
        {
            StartCoroutine(DisparandoRayo());
            rayotrigger.SetActive(false);
        }
    }

    public void SeguirJugador()
    {
        animator.SetBool("caminar", true);
        agent.speed = velocidad;
        agent.SetDestination(jugador.transform.position);
    }


    IEnumerator CargaSuperataque()
    {
        yield return new WaitForSeconds(10f);
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverAbajo();
        Superataqueactivo = false;
        animator.SetBool("superataque", false);
        animator.SetBool("caminar", true);

        yield return new WaitForSeconds(3f);
        ocupado = false;

        yield return new WaitForSeconds(20f);
        puedeHacerSuperataque = true;
    }

    IEnumerator CambiarFase()
    {
        yield return new WaitForSeconds(2f);

        if (vidas == 2)
        {
           
            if (ocupado == false)
            {
                StartCoroutine(Terremoto());
            }
        }

        fase++;
        ocupado = false;
    }

    public void lanzarCorrutinaFase()
    {
        StartCoroutine(CambiarFase());
    }

}


