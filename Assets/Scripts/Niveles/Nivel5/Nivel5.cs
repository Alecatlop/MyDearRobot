using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Nivel5 : MonoBehaviour
{
    public GameMana nivel;
    GameObject niveltemplo;
    GameObject nivelbatalla;


    // Start is called before the first frame update
    void Start()
    {
        niveltemplo = GameObject.Find("Nivel templo");
        nivelbatalla = GameObject.Find("Nivel Jefe");
        niveltemplo.SetActive(false);
        nivelbatalla.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nivel.Nivel3();
            nivel.Nivel5();
            ActivarTemplo();
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void ActivarTemplo()
    {
        niveltemplo.SetActive(!niveltemplo.activeSelf);
    }

    public void ActivarBatalla()
    {
        nivelbatalla.SetActive(!nivelbatalla.activeSelf);
    }

}
