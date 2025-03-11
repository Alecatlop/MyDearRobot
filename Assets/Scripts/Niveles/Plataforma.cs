using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 1);
        StartCoroutine(Movimiento());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Movimiento()
    {
        while (true) 
        {
            if (rand == 0)
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime);

                yield return new WaitForSeconds(2f);

                this.transform.Translate(Vector3.back * Time.deltaTime);

                yield return new WaitForSeconds(2f);
            }
            else
            {
                this.transform.Translate(Vector3.back * Time.deltaTime);

                yield return new WaitForSeconds(2f);

                this.transform.Translate(Vector3.forward * Time.deltaTime);

                yield return new WaitForSeconds(2f);
            }
        }
       
    }
}
