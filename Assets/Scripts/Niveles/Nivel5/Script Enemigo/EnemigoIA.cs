using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;  

public class EnemigoIA: MonoBehaviour
{
    Estado FSM;
    public GameObject jugador;
    public GameObject centro;
    public NavMeshAgent agent;
    GameObject rayo;
    GameObject instancia;
    //public GameObject canon;
    //public bool disparando;
    float dist;
    public int vidas = 3;
    bool ocupado = false;
    
    float fuerza = 30f;
    public Ray ray;
    

    void Start()
    {
        FSM = new Fase1();
        FSM.inicializarVariables(this);
    }

    void Update()
    {
       
        FSM = FSM.Procesar();
    }

 
    //IEnumerator recarga()
    //{
       
    //    while (true)
    //    {
    //        this.agent.speed = 0f;
    //        this.agent.SetDestination(jugador.transform.position);
            
    //        instancia = Instantiate(rayo, canon.transform.position, Quaternion.identity);
    //        instancia.transform.LookAt(jugador.transform.position);
    //        instancia.transform.Rotate(0,90, 90);

    //        instancia.GetComponent<Rigidbody>().AddForce(transform.forward * fuerza, ForceMode.Impulse);
    //        yield return new WaitForSeconds(2f);
    //    }
       
    //}

    public bool PuedeAtacar()
    {
        dist = Vector3.Distance(jugador.transform.position, transform.position);
        
        if (dist > 28f && vidas == 1 && ocupado == false)
        {
            int probabilidad = Random.Range(0, 3);

            if (probabilidad > 0 && ocupado == false)
            {
                ocupado = true;
                Ataque3();
            }
            else if (probabilidad == 0 && ocupado == false)
            {
                ocupado = true;
                Ataque2();
                return true;
            }

            return true;
        }
        else if (dist < 2.5f && ocupado == false)
        {
            ocupado = true;
            Ataque1();
            return true;
        }
        else if (dist > 28f && vidas == 3 && ocupado == false)
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
        if (vidas == 1)
        {
            StartCoroutine(Espera2());
        }

        Debug.Log("Golpearsuelo / TERREMOTO");
        this.agent.SetDestination(centro.transform.position);
       
    }

    public void Dañado()
    {
        vidas--;
        Debug.Log("Dañado, le quedan " + vidas);
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
            Dañado();
        }
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(3f);

        ocupado = false;
        print("listo");
    }

    IEnumerator Espera2()
    {
        yield return new WaitForSeconds(20f);

        ocupado = false;
        print("listo");
    }
}

