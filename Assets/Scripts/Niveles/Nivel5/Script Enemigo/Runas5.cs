using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runas5 : MonoBehaviour
{
    //Material luz;
    public EnemigoIA scriptenemigo;

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
        // se activan al pisar 1 fase
        if (other.name == "RAYO" && scriptenemigo.vidas == 3)
        {
            Material mat = this.GetComponent<MeshRenderer>().material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.yellow);
        }

        // se desactiva al recibir daño jefe
        if (other.name == "RAYO" && scriptenemigo.vidas == 2)
        {
            print("Apagado");
            //luz.material.SetColor("_EmissionColor", Color.white);
        }




    }
}
