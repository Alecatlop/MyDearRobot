using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baldosa : MonoBehaviour
{
    public bool correcto;
    public Renderer runarenderer;
    

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
                Debug.Log("hola");
                Material material = runarenderer.material;
                material.SetColor("_EmissionColor", Color.green);  // Establece el color de emisi�n
                material.EnableKeyword("_EMISSION");  // Aseg�rate de que la emisi�n est� activada
            }

            else
            {
                Material material = runarenderer.material;
                material.SetColor("_EmissionColor", Color.red);  // Establece el color de emisi�n a rojo
                material.EnableKeyword("_EMISSION");
            }

        }
    }
}
