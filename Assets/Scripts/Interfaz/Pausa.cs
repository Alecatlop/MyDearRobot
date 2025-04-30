using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausa : MonoBehaviour
{
    GameObject controles;
    GameObject teclado;
    GameObject ps4;
    GameObject xbox;
    GameObject opciones;
    GameObject configuracion;
    GameObject sliderbrillo;
    public Image panelbrillo;

    GameObject sliderefectos;
    GameObject slidermusica;

    Toggle toggle;

    public AudioSource boton;
    Persistente musica;
    public CharacterControllerScript personaje;

    // Start is called before the first frame update
    void Start()
    {
        controles = GameObject.Find("Controles");
        configuracion = GameObject.Find("Configuracion");
        teclado = GameObject.Find("Teclado");
        ps4 = GameObject.Find("Ps4");
        xbox = GameObject.Find("Xbox");
        opciones = GameObject.Find("Opciones");

        sliderbrillo = GameObject.Find("SliderBrillo");
        sliderefectos = GameObject.Find("SliderEfectos");
        slidermusica = GameObject.Find("SliderMusica");
        toggle = GameObject.Find("Toggle").gameObject.GetComponent<Toggle>();

        musica = GameObject.Find("Persistente").GetComponent<Persistente>();

        this.gameObject.SetActive(false);
        opciones.SetActive(true);
        controles.SetActive(false);
        configuracion.SetActive(false);
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);

        boton.volume = sliderefectos.GetComponent<Slider>().value;
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);

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

    public void Pausar()
    {
        personaje.pausar = !personaje.pausar;

        opciones.SetActive(true);
        controles.SetActive(false);
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);
        configuracion.SetActive(false);

        if (personaje.pausar == true)
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
        boton.Play();
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        personaje.pausar = false;
    }

    public void Controles()
    {
        boton.Play();
        controles.SetActive(true);
        opciones.SetActive(false);
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
        controles.SetActive(false);
        
    }

    public void Teclado()
    {
        boton.Play();
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);
    }

    public void Ps4()
    {
        boton.Play();
        teclado.SetActive(false);
        ps4.SetActive(true);
        xbox.SetActive(false);
    }

    public void Xbox()
    {
        boton.Play();
        teclado.SetActive(false);
        ps4.SetActive(false);
        xbox.SetActive(true);
    }

    public void Volver()
    {
        boton.Play();
        SceneManager.LoadScene("Menu");
    }
}
