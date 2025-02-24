using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    int contadorrunas = -1;
    GameObject puerta;
    public GameObject[] simbolos;

    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Puerta1");
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
            simbolos[contadorrunas].GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        if (contadorrunas == 2)
        {
            puerta.SetActive(false);
        }
    }

}
