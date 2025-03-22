using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    bool subir = false;
    bool bajar = false;
    float speed = 5f;
    int rand;
   

    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0,2);
    }

    // Update is called once per frame
    void Update()
    {
        if (subir == true)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

        if (bajar == true)
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }

    void OnEnable()
    {
        StartCoroutine(Corrutina());
    }

    private IEnumerator Corrutina()
    {
        while (true)
        {
            if (rand == 0)
            {
                subir = true;

                yield return new WaitForSeconds(1f);

                subir = false;

                yield return new WaitForSeconds(2f);

                bajar = true;

                yield return new WaitForSeconds(1f);

                bajar = false;

                yield return new WaitForSeconds(2f);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);

                yield return new WaitForSeconds(2f);

                transform.GetChild(0).gameObject.SetActive(false);

                yield return new WaitForSeconds(2f);
            }
           
        }
       
    }
}
