using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    int contadorrunas = -1;
    public GameMana nivel;
    GameObject puerta;
    


    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Puerta1");
        puerta.GetComponent<Collider>().enabled = false;
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
            puerta.GetComponent<Collider>().enabled = true;
            puerta.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puerta.SetActive(true);
            nivel.Nivel1();
            nivel.Nivel2();
        }
    }

}
