using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    bool subir = false;
    bool bajar = false;
    float speed = 9f;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(Corrutina());
    }

    private IEnumerator Corrutina()
    {
        this.GetComponentInChildren<GameObject>().SetActive(false);

        yield return new WaitForSeconds(2f);

        this.GetComponentInChildren<GameObject>().SetActive(true);

        yield return new WaitForSeconds(2f);
    }
}
