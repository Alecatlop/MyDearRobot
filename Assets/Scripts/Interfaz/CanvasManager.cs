using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    GameObject configuracion;
    GameObject opciones;

    GameObject sliderbrillo;
    float numbrillo;
    public Image panelbrillo;

    GameObject sliderefectos;
    float numefectos;
    

    // Start is called before the first frame update
    void Start()
    {
        configuracion = GameObject.Find("Configuracion");
        opciones = GameObject.Find("Opciones");

        sliderbrillo = GameObject.Find("SliderBrillo");
        sliderefectos = GameObject.Find("SliderEfectos");
        

        configuracion.SetActive(false);
        opciones.SetActive(true);

        sliderbrillo.GetComponent<Slider>().value = PlayerPrefs.GetFloat("brillo", 0f);
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Catret");
    }

    public void Configuracion()
    {
        configuracion.SetActive(true);
        opciones.SetActive(false);
    }

    public void Brillo(float valor)
    {
        numbrillo = valor;
        PlayerPrefs.GetFloat("brillo", sliderbrillo.GetComponent<Slider>().value);
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);
    }

    public void Efectos(float valor)
    {
        numefectos = valor;
        PlayerPrefs.GetFloat("efectos", numefectos);
        AudioListener.volume = sliderefectos.GetComponent<Slider>().value;
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
