using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    int contadorrunas = -1;
    public GameMana nivel;
    public GameObject puerta;
    public GameObject puertasalida;
    public GameObject tierra;
    public GameObject arena;
    public Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Modelo ruinas");
        puerta.GetComponent<Animator>().enabled = false;
        arena = GameObject.Find("Arena");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void RunaColor()
    {
        if (contadorrunas < 2)
        {
            contadorrunas++;
        }

        if (contadorrunas == 2)
        {
            puerta.GetComponent<Animator>().enabled = true;
            animator.Play(stateName: "Animacion_Completa");
            tierra.GetComponent<Collider>().enabled = true;

            tierra.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            arena.SetActive(false);
            puertasalida.SetActive(true);
            animator.Play(stateName: "PuertaCerrar");
            nivel.Nivel1();
            nivel.Nivel2();
            this.GetComponent<Collider>().enabled = false;
        }
    }

}
