using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    GameObject player;
    
    public GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Jugador");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            player.transform.position = spawn.transform.position;
        }
       
    }
}
