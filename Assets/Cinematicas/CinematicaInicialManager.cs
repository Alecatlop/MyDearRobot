using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CinematicaInicialManager : MonoBehaviour
{
    public GameObject secuencia1camara;
    public GameObject secuencia2A;
    public GameObject secuencia2camara;
    public GameObject secuencia3A;
    public GameObject secuencia3camara;
    public GameObject secuencia4A;
    public GameObject secuencia4camara;
    public GameObject secuencia5A;
    public GameObject secuencia5camara;
    public GameObject secuencia6A;
    public GameObject secuencia6camara;
    public GameObject secuencia7A;
    public GameObject secuencia7camara;
    public GameObject secuencia8camara;
    public GameObject secuencia9A;
    public GameObject secuencia9camara;
    public AudioSource audioCinematica;
    public AudioSource audioMusicaCinematica;

    // Start is called before the first frame update
    void Start()
    {
        secuencia1camara.SetActive(false);
        secuencia2A.SetActive(false);
        secuencia2camara.SetActive(false);
        secuencia3A.SetActive(false);
        secuencia3camara.SetActive(false);
        secuencia4A.SetActive(false);
        secuencia4camara.SetActive(false);
        secuencia5A.SetActive(false);
        secuencia5camara.SetActive(false);
        secuencia6A.SetActive(false);
        secuencia6camara.SetActive(false);
        secuencia7A.SetActive(false);
        secuencia7camara.SetActive(false);
        secuencia8camara.SetActive(false);
        secuencia9A.SetActive(false);
        secuencia9camara.SetActive(false);

        var persistente = GameObject.Find("Persistente");
        
        if (persistente != null)
        {
            var p = persistente.GetComponent<Persistente>();
            p.GetComponent<AudioSource>().Stop();
            audioMusicaCinematica.volume = p.volumenmusica;
        }

        audioCinematica.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;

        audioCinematica.PlayDelayed(0f); 
        audioMusicaCinematica.Play(); 

        StartCoroutine(Escena1());
    }
    
    public IEnumerator Escena1()
    {
        secuencia1camara.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        secuencia1camara.SetActive(false);
        secuencia2camara.SetActive(true);
        yield return new WaitForSeconds(1f);
        secuencia2A.SetActive(true);
        yield return new WaitForSeconds(4f);
        secuencia2A.SetActive(false);
        secuencia2camara.SetActive(false);
        secuencia3camara.SetActive(true);
        secuencia3A.SetActive(true);
        yield return new WaitForSeconds(4f);
        secuencia3camara.SetActive(false);
        secuencia3A.SetActive(false);
        secuencia4A.SetActive(true);
        secuencia4camara.SetActive(true);
        yield return new WaitForSeconds(5f);
        secuencia4A.SetActive(false);
        secuencia4camara.SetActive(false);
        secuencia5A.SetActive(true);
        secuencia5camara.SetActive(true);
        yield return new WaitForSeconds(4f);
        secuencia5A.SetActive(false);
        secuencia5camara.SetActive(false);
        secuencia6A.SetActive(true);
        secuencia6camara.SetActive(true);
        yield return new WaitForSeconds(2f);
        secuencia6A.SetActive(false);
        secuencia6camara.SetActive(false);
        secuencia7A.SetActive(true);
        secuencia7camara.SetActive(true);
        yield return new WaitForSeconds(2f);
        secuencia8camara.SetActive(true);
        secuencia7A.SetActive(false);
        secuencia7camara.SetActive(false);
        yield return new WaitForSeconds(3f);
        secuencia8camara.SetActive(false);
        secuencia9A.SetActive(true);
        secuencia9camara.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName: "Oficial");
    }
}
