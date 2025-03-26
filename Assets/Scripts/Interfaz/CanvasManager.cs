using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    GameObject configuracion;
    GameObject opciones;

    GameObject sliderbrillo;
    public Image panelbrillo;

    Toggle toggle;

    GameObject sliderefectos;
    GameObject slidermusica;

    public AudioSource boton;
    Persistente musica;


    // Start is called before the first frame update
    void Start()
    {
        configuracion = GameObject.Find("Configuracion");
        opciones = GameObject.Find("Opciones");

        musica = GameObject.Find("Persistente").GetComponent<Persistente>();

        sliderbrillo = GameObject.Find("SliderBrillo");
        sliderefectos = GameObject.Find("SliderEfectos");
        slidermusica = GameObject.Find("SliderMusica");

        toggle = GameObject.Find("Toggle").gameObject.GetComponent<Toggle>();

        configuracion.SetActive(false);
        opciones.SetActive(true);

        boton.volume = sliderefectos.GetComponent<Slider>().value;
        musica.GetComponent<AudioSource>().volume = slidermusica.GetComponent<Slider>().value;


        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else toggle.isOn = false;

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Jugar()
    {
        boton.Play();
        SceneManager.LoadScene("Catret");
    }

    public void Configuracion()
    {
        boton.Play();
        configuracion.SetActive(true);
        opciones.SetActive(false);
    }

    public void Brillo(float valor)
    {
        PlayerPrefs.GetFloat("brillo", sliderbrillo.GetComponent<Slider>().value);
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);
    }

    public void Efectos(float valor)
    {
        boton.Play();
        PlayerPrefs.GetFloat("efectos", sliderefectos.GetComponent<Slider>().value);
        boton.volume = sliderefectos.GetComponent<Slider>().value;
    }

    public void Musica(float valor)
    {
        PlayerPrefs.GetFloat("musica", slidermusica.GetComponent<Slider>().value);
        musica.GetComponent<AudioSource>().volume = slidermusica.GetComponent<Slider>().value;
    }

    public void PantallaCompleta(bool valor)
    {
        Screen.fullScreen = valor;
    }

    public void Regresar()
    {
        boton.Play();
        configuracion.SetActive(false);
        opciones.SetActive(true);
    }

    public void Salir()
    {
        boton.Play();
        Application.Quit();
    }
}
