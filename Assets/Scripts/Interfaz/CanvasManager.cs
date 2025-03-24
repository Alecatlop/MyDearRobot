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
    public Image panelbrillo;

    GameObject sliderefectos;
    GameObject slidermusica;

    public AudioSource boton;
    public AudioSource musica;


    // Start is called before the first frame update
    void Start()
    {
        configuracion = GameObject.Find("Configuracion");
        opciones = GameObject.Find("Opciones");

        sliderbrillo = GameObject.Find("SliderBrillo");
        sliderefectos = GameObject.Find("SliderEfectos");
        slidermusica = GameObject.Find("SliderMusica");

        configuracion.SetActive(false);
        opciones.SetActive(true);

        slidermusica.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musica", 0.5f);
        sliderefectos.GetComponent<Slider>().value = PlayerPrefs.GetFloat("efectos", 0.5f);

        sliderbrillo.GetComponent<Slider>().value = PlayerPrefs.GetFloat("brillo", 0f);
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);
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
        PlayerPrefs.GetFloat("efectos", sliderefectos.GetComponent<Slider>().value);
        boton.volume = sliderefectos.GetComponent<Slider>().value;
    }

    public void Musica(float valor)
    {
        PlayerPrefs.GetFloat("musica", slidermusica.GetComponent<Slider>().value);
        musica.volume = slidermusica.GetComponent<Slider>().value;
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
