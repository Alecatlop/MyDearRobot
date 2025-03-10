using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Movimiento());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Movimiento()
    {
        int rand = Random.Range(0,1);

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
