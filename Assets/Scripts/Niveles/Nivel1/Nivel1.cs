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
    


    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Modelo ruinas");
        puerta.GetComponent<Animator>().enabled = false;
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
            tierra.GetComponent<Collider>().enabled = true;

            tierra.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puertasalida.SetActive(true);
            puerta.SetActive(false);
            nivel.Nivel1();
            nivel.Nivel2();
            this.GetComponent<Collider>().enabled = false;
        }
    }

}
