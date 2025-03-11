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

    public Nivel1 accion1;
    public Nivel2 accion2;
    public GameMana gamemana;
    public Pausa pausa;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
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

        if (other.tag == "puerta")
        {
            other.transform.gameObject.SetActive(true);
            other.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Puerta2" && ground == false)
        {
            accion2.Abrirpuerta();
        }
    }
}
