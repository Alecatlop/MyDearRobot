using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    Toggle togglebrillo;

    GameObject sliderefectos;
    GameObject slidermusica;

    public AudioSource boton;
    Persistente datos;

    public TMP_Dropdown calidaddropdown;
    Resolution[] resoluciones;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        configuracion = GameObject.Find("Configuracion");
        opciones = GameObject.Find("Opciones");

        datos = GameObject.Find("Persistente").GetComponent<Persistente>();

        sliderbrillo = GameObject.Find("SliderBrillo");
        sliderefectos = GameObject.Find("SliderEfectos");
        slidermusica = GameObject.Find("SliderMusica");

        togglebrillo = GameObject.Find("Toggle").gameObject.GetComponent<Toggle>();

        configuracion.SetActive(false);
        opciones.SetActive(true);


        sliderbrillo.GetComponent<Slider>().value = datos.valorcalidad;
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);

        slidermusica.GetComponent<Slider>().value = datos.volumenmusica;
        datos.GetComponent<AudioSource>().volume = slidermusica.GetComponent<Slider>().value;

        sliderefectos.GetComponent<Slider>().value = datos.volumenefectos;



        if (Screen.fullScreen)
        {
            togglebrillo.isOn = true;
        }
        else togglebrillo.isOn = false;

        ComprobacionCalidad();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ComprobacionCalidad()
    {
        resoluciones = Screen.resolutions;
        calidaddropdown.ClearOptions();
        List<string> calidadtipos = new List<string>();
        int calidadvalor = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string tipo = resoluciones[i].width + " x " + resoluciones[i].height;
            calidadtipos.Add(tipo);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && Screen.fullScreen && resoluciones[i].height == Screen.currentResolution.height)
            {
                calidadvalor = i;
            }
        }

        calidaddropdown.AddOptions(calidadtipos);
        calidaddropdown.value = calidadvalor;
        calidaddropdown.RefreshShownValue();

        calidaddropdown.value = PlayerPrefs.GetInt("numerosresolucion", 0);
    }

    public void CambiarCalidad(int valor)
    {
        PlayerPrefs.SetInt("numerosresolucion", calidaddropdown.value);

        Resolution resolucion = resoluciones[valor];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void Jugar()
    {
        boton.Play();
        SceneManager.LoadScene("Oficial");
    }

    public void Configuracion()
    {
        boton.Play();
        configuracion.SetActive(true);
        opciones.SetActive(false);
    }

    public void Brillo(float valor)
    {
        datos.valorcalidad = sliderbrillo.GetComponent<Slider>().value;
        PlayerPrefs.GetFloat("brillo", sliderbrillo.GetComponent<Slider>().value);
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);
    }

    public void Efectos(float valor)
    {
        datos.volumenefectos = sliderefectos.GetComponent<Slider>().value;
        PlayerPrefs.GetFloat("efectos", sliderefectos.GetComponent<Slider>().value);
        boton.volume = sliderefectos.GetComponent<Slider>().value;
        boton.Play();
    }

    public void Musica(float valor)
    {
        datos.volumenmusica = slidermusica.GetComponent<Slider>().value;
        PlayerPrefs.GetFloat("musica", slidermusica.GetComponent<Slider>().value);
        datos.GetComponent<AudioSource>().volume = slidermusica.GetComponent<Slider>().value;
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
