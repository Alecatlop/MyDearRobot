using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    Persistente datos;
    public CharacterControllerScript personaje;

    public TMP_Dropdown calidaddropdown;
    Resolution[] resoluciones;

    public static bool juegoPausado = false;

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

        datos = GameObject.Find("Persistente").GetComponent<Persistente>();

        this.gameObject.SetActive(false);
        opciones.SetActive(true);
        controles.SetActive(false);
        configuracion.SetActive(false);
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);

        sliderbrillo.GetComponent<Slider>().value = datos.valorcalidad;
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, sliderbrillo.GetComponent<Slider>().value);

        slidermusica.GetComponent<Slider>().value = datos.volumenmusica;
        datos.GetComponent<AudioSource>().volume = slidermusica.GetComponent<Slider>().value;

        sliderefectos.GetComponent<Slider>().value = datos.volumenefectos;

        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else toggle.isOn = false;

        ComprobacionCalidad();
        
        if (!datos.GetComponent<AudioSource>().isPlaying)
        {
            datos.GetComponent<AudioSource>().Play();
        }
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

    public void Pausar()
    {
        personaje.pausar = !personaje.pausar;

        opciones.SetActive(true);
        controles.SetActive(false);
        teclado.SetActive(true);
        ps4.SetActive(false);
        xbox.SetActive(false);
        configuracion.SetActive(false);

        if (personaje.pausar)
        {
            juegoPausado = true;
            PausarEfectos();
            Time.timeScale = 0;
            this.gameObject.SetActive(true);
        }
        else
        {
            juegoPausado = false;
            ReanudarEfectos();
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Continuar()
    {
        boton.Play();
        ReanudarEfectos();
        juegoPausado = false;
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
        datos.valorcalidad = valor;
        PlayerPrefs.SetFloat("brillo", valor);
        PlayerPrefs.Save();
        panelbrillo.color = new Color(panelbrillo.color.r, panelbrillo.color.g, panelbrillo.color.b, valor);
    }
    
    public void Efectos(float valor)
    {
        datos.volumenefectos = valor;
        PlayerPrefs.SetFloat("efectos", valor);
        PlayerPrefs.Save();

        // Actualizar todos los objetos con EfectoSonido
        foreach (EfectoSonido efecto in FindObjectsOfType<EfectoSonido>())
        {
            efecto.SetVolumen(valor);
        }

        // Actualizar botones (si tienen UIButtonSound)
        foreach (UIButtonSound btn in FindObjectsOfType<UIButtonSound>())
        {
            btn.ActualizarVolumen();
        }
    }

    public void Musica(float valor)
    {
        datos.volumenmusica = valor;
        PlayerPrefs.SetFloat("musica", valor);
        PlayerPrefs.Save();
        datos.GetComponent<AudioSource>().volume = valor;
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
    
    void PausarEfectos()
    {
        foreach (var efecto in FindObjectsOfType<EfectoSonido>())
        {
            efecto.Pausar();
        }
    }

    void ReanudarEfectos()
    {
        foreach (var efecto in FindObjectsOfType<EfectoSonido>())
        {
            efecto.Reanudar();
        }
    }
}
