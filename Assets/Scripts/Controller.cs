using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    Vector2 inputmove;
    Vector2 inputaim;
    Rigidbody rb;
    float speed = 7f;
    float force = 100f;
    Vector2 sensibilidad = new Vector2(60, 40);
    bool ground = true;

    GameObject nivel1;
    GameObject nivel2;

    public Nivel1 accion1;
    public Nivel2 accion2;
    public Pausa pausa;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        nivel1 = GameObject.Find("Nivel1 Manager");
        nivel2 = GameObject.Find("Nivel2 Manager");
    }

    // Update is called once per frame
    void Update()
    {
        // mover jugador
        Vector3 newVelocity = new Vector3(inputmove.x * speed, rb.velocity.y, inputmove.y * speed);
        newVelocity = transform.rotation * newVelocity;
        rb.velocity = newVelocity;

        // rotar personaje con cámara
        transform.Rotate(0,inputaim.x * sensibilidad.x * Time.deltaTime,0);
        //transform.GetChild(0).Rotate(-inputaim.y * sensibilidad.y * Time.deltaTime, 0, 0);
        //transform.GetChild(0).localRotation = Quaternion.Euler(xAngle, 0, 0);
    }

    private void OnMove(InputValue value)
    {
        inputmove = value.Get<Vector2>();     
    }

    private void OnAim(InputValue value)
    {
        inputaim = value.Get<Vector2>();
    }

    private void OnAim2(InputValue value)
    {
        inputaim = value.Get<Vector2>();
    }

    private void OnPause()
    {
        pausa.Pausar();
    }

    private void OnJump()
    {
        if (ground == true)
        {
            rb.AddForce(0, speed * force, 0);
            ground = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ground = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "runa nivel1")
        {
            other.GetComponent<MeshRenderer>().material.color = Color.yellow;
            other.GetComponent<Collider>().enabled = false;
            accion1.RunaColor();
        }

        if (other.name == "Puerta3")
        {
            accion2.Abrirpuerta2();
        }

        if (other.tag == "puerta")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.GetComponent<Collider>().enabled = false;

            if (other.name == "trigger puerta 2")
            {
                print("nivel 1 fuera");
                nivel1.SetActive(false);
            }

            if (other.name == "trigger puerta 4")
            {
                print("nivel 2 fuera");
                nivel2.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Puerta2" && ground == false)
        {
            accion2.Abrirpuerta();
        }

        if (other.name == "Puerta4" && ground == false)
        {
            accion2.Abrirpuerta();
        }
    }
}
