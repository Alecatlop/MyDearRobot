using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderData;

public class Pausa : MonoBehaviour
{
    GameObject controles;
    GameObject teclado;
    GameObject ps4;
    GameObject xbox;
    GameObject opciones;
    bool activo = false;

    // Start is called before the first frame update
    void Start()
    {
        controles = GameObject.Find("Controles");
        teclado = GameObject.Find("Teclado");
        ps4 = GameObject.Find("Ps4");
        xbox = GameObject.Find("Xbox");
        opciones = GameObject.Find("Opciones");

        this.gameObject.SetActive(false);
        opciones.SetActive(true);
        controles.SetActive(false);
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Pausar()
    {
        activo = !activo;

        if (activo == true)
        {
            Time.timeScale = 0;
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Continuar()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Controles()
    {
        controles.SetActive(true);
        opciones.SetActive(false);
    }

    public void Regresar()
    {
        opciones.SetActive(true);
        controles.SetActive(false);
    }

    public void Teclado()
    {
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);
    }

    public void Ps4()
    {
        teclado.SetActive(false);
        ps4.SetActive(true);
        xbox.SetActive(false);
    }

    public void Xbox()
    {
        teclado.SetActive(false);
        ps4.SetActive(false);
        xbox.SetActive(true);
    }

    public void Volver()
    {
        SceneManager.LoadScene("Menu");
    }
}
