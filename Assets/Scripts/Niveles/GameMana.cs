using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMana : MonoBehaviour
{
   
    public GameObject[] niveles;

    // Start is called before the first frame update
    void Start()
    {
    
        niveles[1].SetActive(false);
        niveles[2].SetActive(false);
        niveles[3].SetActive(false);
        niveles[4].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Nivel1()
    {
        Debug.Log("activado.");
        StartCoroutine(Nivel1Destroy());
    }

    public void Nivel2()
    {
        Debug.Log("activado.");
        niveles[1].SetActive(!niveles[1].activeSelf);
    }

    public void Nivel3()
    {
        Debug.Log("activado.");
        niveles[2].SetActive(!niveles[2].activeSelf);
    }

    public void Nivel4()
    {
        Debug.Log("activado.");
        niveles[3].SetActive(!niveles[3].activeSelf);
    }

    public void Nivel5()
    {
        Debug.Log("activado.");
        niveles[4].SetActive(!niveles[4].activeSelf);
    }

   /* public void Nivel6()
    {
                Debug.Log("activado.");

        niveles[5].SetActive(!niveles[5].activeSelf);
    }
   */
    public IEnumerator Nivel1Destroy()
    {
        yield return new WaitForSeconds(2f);
        niveles[0].SetActive(!niveles[0].activeSelf);

    }
}
