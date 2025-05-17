using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaFinalMManager : MonoBehaviour
{
    public GameObject fade;
    public GameObject fadeOut;
    public GameObject secuencia1A;
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

    // Start is called before the first frame update
    void Start()
    {
        fadeOut.SetActive(false);
        secuencia1A.SetActive(false);
        secuencia1camara.SetActive(false);
        secuencia2A.SetActive(false);
        secuencia2camara.SetActive(false);
        secuencia3A.SetActive(false);
        secuencia3camara.SetActive(false);
        secuencia4A.SetActive(false);
        secuencia4camara.SetActive(false);
        secuencia5A.SetActive(false);
        secuencia5camara.SetActive(false);
        /*secuencia6A.SetActive(false);
        secuencia6camara.SetActive(false);
        secuencia7A.SetActive(false);
        secuencia7camara.SetActive(false);
        secuencia8camara.SetActive(false);
        secuencia9A.SetActive(false);
        secuencia9camara.SetActive(false); */


        StartCoroutine(Escena1());

    }

    public IEnumerator Escena1()
    {
        secuencia1A.SetActive(true);
        secuencia1camara.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        secuencia1camara.SetActive(false);
        secuencia2camara.SetActive(true);
        secuencia2A.SetActive(true);
        secuencia1A.SetActive(false);
        yield return new WaitForSeconds(4.5f);
        fade.SetActive(false);
        yield return new WaitForSeconds(2f);
        secuencia2camara.SetActive(false);
        secuencia3A.SetActive(true);
        secuencia3camara.SetActive(true);
        yield return new WaitForSeconds(3f);
        secuencia2A.SetActive(false);
        secuencia3A.SetActive(false);
        secuencia3camara.SetActive(false);
        secuencia4A.SetActive(true);
        secuencia4camara.SetActive(true);
        yield return new WaitForSeconds(4f);
        secuencia4A.SetActive(false);
        secuencia4camara.SetActive(false);
        secuencia5A.SetActive(true);
        secuencia5camara.SetActive(true);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2f);
        fadeOut.SetActive(false);





    }
}
