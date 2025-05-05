using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public GameObject camara;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator Animacion()
    {
        yield return new WaitForSeconds(1f);
        camara.SetActive(true);
        yield return new WaitForSeconds(5f);
        camara.SetActive(false);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            StartCoroutine(Animacion());
        }
    }
}
