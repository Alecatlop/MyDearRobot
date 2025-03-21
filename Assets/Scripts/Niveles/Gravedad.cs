using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedad : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gravedad = false;
    
    void OnTriggerEnter(Collider other)
    {
        if(gravedad == false)
        {
            gravedad = true;
            Physics.gravity = new Vector3(0, 9.8f, 0);           
        }

        else if(gravedad == true)
        {
            gravedad = false;
            Physics.gravity = new Vector3(0,-9.8f, 0);

        }
    }
}
