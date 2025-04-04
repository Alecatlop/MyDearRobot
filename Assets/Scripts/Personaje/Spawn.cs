using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    public CharacterControllerScript player;
    public GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.altura == true)
        {
            player.gameObject.transform.position = spawn.transform.position;
            player.altura = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && player.respawn == true)
        {
            player.gameObject.transform.position = spawn.transform.position;
        }
        else this.GetComponent<Collider>().enabled = false; 
       
    }
}
