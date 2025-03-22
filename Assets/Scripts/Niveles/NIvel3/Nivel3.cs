using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel3 : MonoBehaviour
{
    public GameMana nivel;
    GameObject ruta1;
    GameObject ruta2;
    GameObject ruta3;
    GameObject pasillo;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        ruta1 = GameObject.Find("Ruta1");
        ruta2 = GameObject.Find("Ruta2");
        ruta3 = GameObject.Find("Ruta3");
        pasillo = GameObject.Find("Pasillo 1");

        ruta1.SetActive(false);
        ruta2.SetActive(false);
        ruta3.SetActive(false);

        rand = Random.Range(0, 3);

        if (rand == 0)
        {
            ruta1.SetActive(true);
        }
        else if (rand == 1)
        {
            ruta2.SetActive(true);
        }
        else ruta3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pasillo.SetActive(false);
            nivel.Nivel2();
            nivel.Nivel4();
        }
    }
}
