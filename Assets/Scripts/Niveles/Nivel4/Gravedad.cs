using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedad : MonoBehaviour
{
    public CharacterControllerScript jugador;
    public bool gravedad = false;

    void OnTriggerEnter(Collider other)
    {
        if(jugador.gravitycheck == true)
        {
            jugador.gravitycheck = false;
            Physics.gravity = new Vector3(0, 9.8f, 0);
            gravedad = true;
        }

        else
        {
            jugador.gravitycheck = true;
            Physics.gravity = new Vector3(0, -9.8f, 0);
            gravedad = false;
        }
    }
}
