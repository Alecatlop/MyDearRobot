using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    int contadorrunas = -1;
    public GameObject[] simbolos;
    public GameMana nivel;

    // Start is called before the first frame update
    void Start()
    {

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
            nivel.Abrir1();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nivel.Abrir1();
            nivel.Nivel1();
            nivel.Nivel2();
        }
    }

}
