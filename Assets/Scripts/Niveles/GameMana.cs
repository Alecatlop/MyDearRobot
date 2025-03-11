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

    // Start is called before the first frame update
    void Start()
    {
        nivel1 = GameObject.Find("Nivel1 Manager");
        nivel2 = GameObject.Find("Nivel2 Manager");
        //nivel3 = GameObject.Find("Nivel3 Manager");
        //nivel4 = GameObject.Find("Nivel4 Manager");
        //nivel5 = GameObject.Find("Nivel5 Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Nivel1()
    {
        nivel1.SetActive(false);
    }

    public void Nivel2()
    {
        nivel2.SetActive(false);
    }

    //public void Nivel3()
    //{
    //    nivel3.SetActive(false);
    //}

    //public void Nivel4()
    //{
    //    nivel4.SetActive(false);
    //}

    //public void Nivel5()
    //{
    //    nivel5.SetActive(false);
    //}
}
