using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel4 : MonoBehaviour
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
            nivel.Nivel5();
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
