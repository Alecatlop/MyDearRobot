using System.Collections;
using System.Collections.Generic;
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

        StartCoroutine(Escena1());
        
    }
    
    public IEnumerator Escena1()
    {
        secuencia1camara.SetActive(true);
        yield return new WaitForSeconds(6f);
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


    }
}
