using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baldosa : MonoBehaviour
{
    public bool correcto;
    

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
        if (other.name == "Jugador")
        {
            if (correcto == true)
            {
                this.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else this.gameObject.SetActive(false);
        }
    }
}
