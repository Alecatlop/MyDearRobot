using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel3 : MonoBehaviour
{
    public GameMana nivel;
    GameObject ruta1;
    GameObject ruta2;
    GameObject ruta3;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        ruta1 = GameObject.Find("Ruta 1");
        ruta2 = GameObject.Find("Ruta 2");
        ruta3 = GameObject.Find("Ruta 3");
        ruta1.SetActive(false);
        ruta2.SetActive(false);
        ruta3.SetActive(false);

        rand = Random.Range(0,3);

        if (rand == 0)
        {
            ruta1.SetActive(true);
        }
        else if(rand == 1)
        {
            ruta2.SetActive(true);
        }
        else ruta3.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pasar()
    {
        
    }

private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nivel.Abrir3();
            nivel.Nivel3();
        }
    }
}
