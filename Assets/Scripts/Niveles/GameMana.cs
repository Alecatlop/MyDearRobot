using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMana : MonoBehaviour
{
    //GameObject nivel1;
    //GameObject nivel2;
    //GameObject nivel3;
    //GameObject nivel4;
    //GameObject nivel5;
    //GameObject nivel6;
    public GameObject[] niveles;

    // Start is called before the first frame update
    void Start()
    {
        //nivel1 = GameObject.Find("Nivel1 Manager");
        //nivel2 = GameObject.Find("Nivel2 Manager");
        //nivel3 = GameObject.Find("Nivel3 Manager");
        //nivel4 = GameObject.Find("Nivel4 Manager");
        //nivel5 = GameObject.Find("Nivel5 Manager");
        //nivel6 = GameObject.Find("Nivel6 Manager");

        //nivel2.SetActive(false);
        //nivel3.SetActive(false);
        //nivel4.SetActive(false);
        //nivel5.SetActive(false);
        //nivel6.SetActive(false);

        niveles[1].SetActive(false);
        niveles[2].SetActive(false);
        niveles[3].SetActive(false);
        niveles[4].SetActive(false);
        niveles[5].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Nivel1()
    {
        //nivel1.SetActive(!nivel1.activeSelf);
        niveles[0].SetActive(!niveles[0].activeSelf);
    }

    public void Nivel2()
    {
        //nivel2.SetActive(!nivel2.activeSelf);
        niveles[1].SetActive(!niveles[1].activeSelf);
    }

    public void Nivel3()
    {
        //nivel3.SetActive(!nivel3.activeSelf);
        niveles[2].SetActive(!niveles[2].activeSelf);
    }

    public void Nivel4()
    {
        //nivel4.SetActive(!nivel4.activeSelf);
        niveles[3].SetActive(!niveles[3].activeSelf);
    }

    public void Nivel5()
    {
        //nivel5.SetActive(!nivel5.activeSelf);
        niveles[4].SetActive(!niveles[4].activeSelf);
    }

    public void Nivel6()
    {
        //nivel6.SetActive(!nivel6.activeSelf);
        niveles[5].SetActive(!niveles[5].activeSelf);
    }
}
