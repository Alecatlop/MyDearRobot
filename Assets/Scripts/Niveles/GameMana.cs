using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMana : MonoBehaviour
{
    GameObject nivel1;
    GameObject nivel2;
    GameObject nivel3;
    GameObject nivel4;
    GameObject nivel5;
    GameObject nivel6;

    public GameObject[] puertas;



    // Start is called before the first frame update
    void Start()
    {
        nivel1 = GameObject.Find("Nivel1 Manager");
        nivel2 = GameObject.Find("Nivel2 Manager");
        nivel3 = GameObject.Find("Nivel3 Manager");
        //nivel4 = GameObject.Find("Nivel4 Manager");
        //nivel5 = GameObject.Find("Nivel5 Manager");
        //nivel6 = GameObject.Find("Nivel6 Manager");
        nivel2.SetActive(false);
        //nivel3.SetActive(false);

        //puerta = GameObject.Find("Puerta3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Nivel1()
    {
        nivel1.SetActive(!nivel1.activeSelf);
    }

    public void Abrir1()
    {
        puertas[0].gameObject.SetActive(!puertas[0].activeSelf);
    }

    public void Nivel2()
    {
        nivel2.SetActive(!nivel2.activeSelf);
    }

    public void Abrir2()
    {
        puertas[1].gameObject.SetActive(!puertas[1].activeSelf);
    }

    public void Nivel3()
    {
        nivel3.SetActive(!nivel3.activeSelf);
    }

    public void Abrir3()
    {
        puertas[2].gameObject.SetActive(!puertas[2].activeSelf);
    }

    //public void Nivel4()
    //{
    //    nivel4.SetActive(!nivel4.activeSelf);
    //}

    //public void Nivel5()
    //{
    //     nivel5.SetActive(!nivel5.activeSelf);
    //}

    //public void Nivel6()
    //{
    //     nivel6.SetActive(!nivel6.activeSelf);
    //}
}
