using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightProbeProxyVolume;

public class Nivel2 : MonoBehaviour
{
    public GameMana nivel;
    GameObject plataformas;
    GameObject puerta;
    GameObject jugador;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        puerta = GameObject.Find("Puerta3");
        plataformas = GameObject.Find("Plataformas");
        plataformas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posjugador = jugador.transform.position;
        Vector3 pospuerta = puerta.transform.position;

        float distancia = Vector3.Distance(posjugador, pospuerta);

        if (distancia <= 3)
        {
           puerta.SetActive(false);
        }
    }

    public void ActivarPlataformas()
    {
        nivel.Abrir2();
        plataformas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nivel.Abrir2();
            nivel.Nivel2();
        }
    }
}
