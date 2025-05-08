using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Nivel5 : MonoBehaviour
{
    public GameMana nivel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nivel.Nivel3();
            nivel.Nivel5();
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
