using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runa : MonoBehaviour
{
    public GameObject runa;
    public bool activado = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Pintar()
    {
        activado = true;
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        runa.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            Pintar();
        }
    }
}
