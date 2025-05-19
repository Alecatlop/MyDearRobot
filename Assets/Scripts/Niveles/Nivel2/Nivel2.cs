using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightProbeProxyVolume;

public class Nivel2 : MonoBehaviour
{
    public GameMana nivel;
    GameObject puerta;
    GameObject plataformas;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        plataformas = GameObject.Find("Plataformas");
        puerta = GameObject.Find("Puerta2");
        
        plataformas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarPlataformas()
    {
        animator.Play("Puerta");
        plataformas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.Play("PuertaCerrar");
            nivel.Nivel3();
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
