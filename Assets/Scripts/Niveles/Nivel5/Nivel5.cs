using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel5 : MonoBehaviour
{
    GameObject pasillo;
    public GameMana nivel;

    // Start is called before the first frame update
    void Start()
    {
        pasillo = GameObject.Find("Pasillo 3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pasillo.SetActive(false);
            nivel.Nivel4();
            nivel.Nivel6();
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
