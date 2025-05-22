using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CinematicaFinalBManager : MonoBehaviour
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
    public GameObject secuencia8A;
    public GameObject secuencia9camara;
    public GameObject secuencia9A;
    public GameObject secuencia10camara;
    public GameObject secuencia10A;
    public GameObject secuencia11camara;
    public GameObject secuencia11A;
    public GameObject creditos;
    public GameObject particulas;


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
        secuencia6A.SetActive(false);
        secuencia6camara.SetActive(false);
        secuencia7A.SetActive(false);
        secuencia7camara.SetActive(false);
        secuencia8camara.SetActive(false);
        secuencia8A.SetActive(false);
        secuencia9camara.SetActive(false);
        secuencia9A.SetActive(false);
        /*secuencia10camara.SetActive(false);
        secuencia10A.SetActive(false);
        secuencia11camara.SetActive(false);
        secuencia11A.SetActive(false);
        creditos.SetActive(false);
        particulas.SetActive(false);*/



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
        yield return new WaitForSeconds(2.1f);
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
        yield return new WaitForSeconds(3f);
        secuencia5A.SetActive(false);
        secuencia5camara.SetActive(false);
        secuencia6A.SetActive(true);
        secuencia6camara.SetActive(true);
        yield return new WaitForSeconds(6f);
        fade.SetActive(true);
        yield return new WaitForSeconds(6.8f);
        particulas.SetActive(true);
        secuencia6A.SetActive(false);
        secuencia6camara.SetActive(false);
        secuencia7A.SetActive(true);
        secuencia7camara.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        fade.SetActive(false);
       yield return new WaitForSeconds(2.6f);
        secuencia7A.SetActive(false);
        secuencia7camara.SetActive(false);
        secuencia8camara.SetActive(true);
        secuencia8A.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        secuencia8camara.SetActive(false);
        secuencia8A.SetActive(false);
        secuencia9camara.SetActive(true);
       secuencia9A.SetActive(true);
        /*yield return new WaitForSeconds(3f);
        secuencia9camara.SetActive(false);
        secuencia9A.SetActive(false);
        secuencia10camara.SetActive(true);
        secuencia10A.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        secuencia10camara.SetActive(false);
        secuencia10A.SetActive(false);
        secuencia11camara.SetActive(true);
        secuencia11A.SetActive(true);
        yield return new WaitForSeconds(1f);
        creditos.SetActive(true);
        yield return new WaitForSeconds(55f);
        creditos.SetActive(true);
        fade.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName: "Menu");*/

    }
}
