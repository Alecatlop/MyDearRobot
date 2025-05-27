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
    
    GameObject camara1;
    GameObject camara2;
    bool camaractivada = true;
    GameObject COspawnfase2;


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
    public bool ocupado = true;
    public bool luzruna;
    public bool puedeHacerSuperataque;
    public bool Superataqueactivo;
    bool puño = false;
    public AudioSource audioPasos;
    public AudioSource audioEfectos;
    public AudioClip pisotonClip;
    public AudioClip pasoClip;
    public AudioClip rayoClip;
    public float tiempoEntrePaso = 0.5f; 
    private float pasoTiempo = 0f;
    float tiempoUltimoPisoton = -10f;
    float cooldownPisoton = 1f;
    private bool puedeSonarRayo = true;

    void Start()
    {
      
        FSM = new Fase1();
        FSM.inicializarVariables(this);
        COspawnfase2 = GameObject.Find("COspawnfase2");
        // spawnfase2 = GameObject.Find("spawnfase2");
        COspawnfase2.GetComponent<BoxCollider>().enabled = false;
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
        particulasuperataque = GameObject.Find("particulas superataque");
        particulasuperataque.SetActive(false);

        camara1 = GameObject.Find("camara 1");
        camara2 = GameObject.Find("camara 2");
        camara1.SetActive(false);
       

        collidersize1 = COpuñetazo.GetComponent<BoxCollider>().size;

        for (int i = 0; i < posicionOcupada.Length; i++)
        {
            posicionOcupada[i] = -1;
        }

        for (int i = 0; i < avisorunas.Length; i++)
        {
            avisorunas[i].SetActive(false);
        }

        transform.LookAt(jugador.transform.position);

        if (audioPasos != null) 
        {
            audioPasos.loop = false;
            audioPasos.Stop();
        }

        StartCoroutine(Intro());
    }

    void Update()
    {
        FSM = FSM.Procesar();

        pasoTiempo -= Time.deltaTime;

        if (animator.GetBool("caminar") && pasoTiempo <= 0f && agent.velocity.magnitude > 0.1f)
        {
            if (!audioPasos.isPlaying)
            {
                audioPasos.PlayOneShot(pasoClip);
                pasoTiempo = tiempoEntrePaso;
            }
        }

        if (runas[runarandom].GetComponent<Runas5>().runapintada == true && fase == 3)
        {
            posicionOcupada[runarandom] = 1;
        }

        if (ocupado == true && puño == true)
        {
            COpuñetazo.GetComponent<BoxCollider>().size = new Vector3(COpuñetazo.GetComponent<BoxCollider>().size.x + 0.9f, COpuñetazo.GetComponent<BoxCollider>().size.y, COpuñetazo.GetComponent<BoxCollider>().size.z + 0.9f);
        }

    }

    public void TerminarCorrutinas()
    { 
        StopAllCoroutines();
    }

    public bool PuedeAtacar()
    {
        dist = Vector3.Distance(jugador.transform.position, transform.position);

       
        if (dist > 35f && vidas == 1 && ocupado == false && jugador.GetComponent<CharacterControllerScript>().daño == false && camaractivada == false)
        {
            int probabilidad = Random.Range(0, 3);

            if (probabilidad > 0 && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false && jugador.GetComponent<CharacterControllerScript>().daño == false && camaractivada == false)
            {
                ocupado = true;
                StartCoroutine(Ataque3());
                return true;
            }
            else if (probabilidad == 0 && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false && jugador.GetComponent<CharacterControllerScript>().daño == false && camaractivada == false)
            {
                ocupado = true;
                StartCoroutine(Ataque2());
                return true;
            }

            return true;
        }
        else if (dist < 15f && ocupado == false && Superataqueactivo == false && puedeHacerSuperataque == false && jugador.GetComponent<CharacterControllerScript>().daño == false && camaractivada == false)
        {
            ocupado = true;
            StartCoroutine(Ataque1());
            return true;
        }
        else if (dist > 35f && vidas == 3 && ocupado == false && jugador.GetComponent<CharacterControllerScript>().daño == false && camaractivada == false)
        {
            ocupado = true;
            StartCoroutine(Ataque2());
            return true;
        }
        else return false;
    }

    IEnumerator Ataque1()
    {
        //transform.position = spawnfase2.transform.position;
        
        this.agent.speed = 0f;
        animator.SetBool("pisoton", true);
        animator.SetBool("caminar", false);
       

        yield return new WaitForSeconds(1.8f);
        particulaspisoton.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        if (jugador.GetComponent<CharacterControllerScript>().daño == false)
        {
            COpisoton.SetActive(true);

            if (audioPasos != null && pisotonClip != null)
            {
                if (Time.time - tiempoUltimoPisoton >= cooldownPisoton)
                {
                    audioPasos.PlayOneShot(pisotonClip);
                    tiempoUltimoPisoton = Time.time;
                }
            }
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
        //CharacterController controller = jugador.GetComponent<CharacterController>();

        //if (controller != null)
        //{


        //    controller.enabled = true;
        //}
        COspawnfase2.GetComponent<BoxCollider>().enabled = true;
        agent.speed = 0;
        animator.SetBool("terremoto", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(1f);
        COspawnfase2.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(4f);
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

        yield return new WaitForSeconds(2f);
        runas[runarandom].GetComponent<Runas5>().AvisoRuna();
        avisorunas[runarandom].SetActive(true);
        animator.SetBool("furia", true);

        yield return new WaitForSeconds(2f);
        animator.SetBool("furia", false);
        animator.SetBool("superataque", true);
        animator.SetBool("caminar", false);

        yield return new WaitForSeconds(3f);
        if (posicionOcupada[runarandom] == -1)
        {
            runas[runarandom].GetComponent<Runas5>().NoAvisoRuna();
        }
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverArriba();
        avisorunas[runarandom].SetActive(false);

        yield return new WaitForSeconds(5.5f);
        COsuperataque.SetActive(true);
        particulasuperataque.SetActive(true);

        yield return new WaitForSeconds(2f);
        COsuperataque.SetActive(false);
        particulasuperataque.SetActive(false);
       
        Superataqueactivo = true;
        puedeHacerSuperataque = false;
        this.agent.speed = 0f;

        StartCoroutine(CargaSuperataque());
    }

    public void ActivarRayo()
    {
        if (contadorrunas == 6 && fase < 3)
        {
            if (fase == 1)
            {
                StartCoroutine(Camara1());
            }
            
            rayotrigger.SetActive(true);
            contadorrunas = 0;
        }
        else if (contadorrunas == 6 && fase == 3)
        {
            StartCoroutine(Camara1());
            orbelaser.SetActive(true);
            rayotrigger.SetActive(true);
        }
    }

    IEnumerator Camara1()
    {
        camara1.SetActive(true);
        camaractivada = true;
        animator.SetBool("caminar", false);
        ocupado = true;
        this.agent.speed = 0f;
        
        yield return new WaitForSeconds(1.5f);
        orbelaser.SetActive(true);

        yield return new WaitForSeconds(3f);
        camaractivada = false;
        ocupado = false;
        camara1.SetActive(false);
        SeguirJugador();
    }

    IEnumerator Camara2()
    {
        camara1.SetActive(true);
        camaractivada = true;
        animator.SetBool("caminar", false);
        ocupado = true;
        this.agent.speed = 0f;
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        camara1.SetActive(false);
        camara2.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        ocupado = false;
        yield return new WaitForSeconds(2f);
        camara2.SetActive(false);
    }

    IEnumerator DisparandoRayo()
    {
        StartCoroutine(Camara2());
        yield return new WaitForSeconds(2.5f);
        ocupado = true;
        rayolaser.SetActive(true);

        if (puedeSonarRayo)
        {
            audioEfectos.PlayOneShot(rayoClip);
            puedeSonarRayo = false;
            StartCoroutine(ResetSonidoDelay());
        }

        yield return new WaitForSeconds(0.5f);
        rayolaserparticulas.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rayolaser.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Dañado());
        orbelaser.SetActive(false);
        rayolaserparticulas.SetActive(false);
    }

    IEnumerator ResetSonidoDelay()
    {
        yield return new WaitForSeconds(1f);  
        puedeSonarRayo = true;
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
            camaractivada = false;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "corayo" && ocupado == false )
        {
            StartCoroutine(DisparandoRayo());
            rayotrigger.SetActive(false);
        }
    }

    public void SeguirJugador()
    {
        if (camaractivada == false)
        {
            animator.SetBool("caminar", true);
            agent.speed = velocidad;
            agent.SetDestination(jugador.transform.position);
        }
    }


    IEnumerator CargaSuperataque()
    {
        yield return new WaitForSeconds(1f);
        platasformas[runarandom].GetComponent<Nivel5Plataformas1>().MoverAbajo();
        Superataqueactivo = false;
        animator.SetBool("superataque", false);

        yield return new WaitForSeconds(1f);
        SeguirJugador();

        yield return new WaitForSeconds(2f);
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

    IEnumerator Intro()
    {
        if (audioPasos != null) audioPasos.Stop();

        camara2.SetActive(true);
        animator.SetBool("furia", true);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(2f);
        camara2.SetActive(false);

        yield return new WaitForSeconds(1f);
        camaractivada = false;
        ocupado = false;
        animator.SetBool("furia", false);
    }

}


