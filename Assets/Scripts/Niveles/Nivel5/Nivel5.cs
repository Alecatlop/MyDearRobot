using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class Nivel5 : MonoBehaviour
{
    public GameMana nivel;
    GameObject niveltemplo;
    GameObject nivelbatalla;
    public Image fade;

    void Awake()
    {
        
    }

    private void Start()
    {
        niveltemplo = GameObject.Find("Nivel Templo");
        nivelbatalla = GameObject.Find("Nivel Batalla");
        nivelbatalla.SetActive(false);
        fade.color = new Color(1, 1, 1, 0);
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
            nivel.Nivel4();
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void ActivarBatalla()
    {
        StartCoroutine(CambioBatalla());
    }

    IEnumerator CambioBatalla()
    {
        while (fade.color.a < 1)
        {
            fade.color = new Color(1, 1, 1, fade.color.a + 0.15f);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        nivelbatalla.SetActive(true);
        niveltemplo.SetActive(false);

        while (fade.color.a > 0)
        {
            fade.color = new Color(1, 1, 1, fade.color.a - 0.15f);

            yield return new WaitForSeconds(0.1f);
        }
    }

}
