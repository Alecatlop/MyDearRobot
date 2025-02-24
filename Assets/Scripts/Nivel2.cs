using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel2 : MonoBehaviour
{
    GameObject puerta;
    GameObject puerta2;
    int a;

    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Puerta2");
        puerta2 = GameObject.Find("Puerta3");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Abrirpuerta()
    {
        
        puerta.SetActive(false);
    }

    public void Abrirpuerta2()
    {
        a++;

        if (a == 1)
        {
            puerta2.SetActive(false);
        }
        else if (a == 2)
        {
            puerta2 .SetActive(true);
        }
    }

    public void Cerrarpuerta()
    {
        puerta.SetActive(true);
    }

}
