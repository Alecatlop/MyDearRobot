using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightProbeProxyVolume;

public class Nivel2 : MonoBehaviour
{
    GameObject puerta;
    public GameMana nivel;
    GameObject plataformas;

    //GameObject puerta2;

    // Start is called before the first frame update
    void Start()
    {
        puerta = GameObject.Find("Puerta2");
        plataformas = GameObject.Find("Plataformas");
        plataformas.SetActive(false);
        //puerta2 = GameObject.Find("Puerta3");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Abrirpuerta()
    {
        puerta.SetActive(false);
        plataformas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puerta.SetActive(true);
            nivel.Nivel2();
        }
    }
}
