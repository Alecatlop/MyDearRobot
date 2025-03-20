using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    public Controller player;
    public GameObject spawn;

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
        if (other.tag == "Player" && player.respawn == true)
        {
            player.gameObject.transform.position = spawn.transform.position;
        }
        else this.GetComponent<Collider>().enabled = false; 
       
    }
}
