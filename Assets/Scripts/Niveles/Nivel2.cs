using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel2 : MonoBehaviour
{
    GameObject puerta;
    public GameMana nivel;
    //GameObject puerta2;

    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Puerta2");
        //puerta2 = GameObject.Find("Puerta3");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Abrirpuerta()
    {
        puerta.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puerta.SetActive(true);
            nivel.Nivel1();
        }
    }
}
