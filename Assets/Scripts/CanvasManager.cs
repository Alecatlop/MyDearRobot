using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    GameObject configuracion;
    GameObject opciones;

    // Start is called before the first frame update
    void Start()
    {
        configuracion = GameObject.Find("Configuracion");
        configuracion.SetActive(false);
        opciones = GameObject.Find("Opciones");
        opciones.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Configuracion()
    {
        configuracion.SetActive(true);
        opciones.SetActive(false);
    }

    public void Regresar()
    {
        configuracion.SetActive(false);
        opciones.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
