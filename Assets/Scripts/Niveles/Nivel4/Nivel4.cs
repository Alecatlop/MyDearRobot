using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel4 : MonoBehaviour
{
    public GameMana nivel;
    GameObject puerta;
    GameObject pasillo;


    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Puerta6");
        pasillo = GameObject.Find("Pasillo 2");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puerta.SetActive(true);
            pasillo.SetActive(false);
            nivel.Nivel3();
            nivel.Nivel5();

        }
    }

}
