using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    bool avanzar = false;
    bool retroceder = false;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (avanzar == true)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        if (retroceder == true)
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
    }

    public void Inicio()
    {
        StartCoroutine(Movimiento());
    }

    private IEnumerator Movimiento()
    {
        while (true) 
        {
            retroceder = true;

            yield return new WaitForSeconds(1f);

            retroceder = false;

            yield return new WaitForSeconds(2f);

            avanzar = true;

            yield return new WaitForSeconds(1f);

            avanzar = false;

            yield return new WaitForSeconds(2f);
        }
       
    }
}
