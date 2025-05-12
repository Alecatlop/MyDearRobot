using System;
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
    float anglex;
    Vector2 sensibilidad = new Vector2(35, 15);
    bool ground = true;
    bool pausar = false;


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


        // rotar personaje con c�mara
        transform.Rotate(0,inputaim.x * sensibilidad.x * Time.deltaTime,0);
        transform.GetChild(0).Rotate(-inputaim.y * sensibilidad.y * Time.deltaTime, 0, 0);
        anglex = Mathf.Clamp(anglex - inputaim.y * sensibilidad.y * Time.deltaTime, -5, 40);
        transform.GetChild(0).localRotation = Quaternion.Euler(anglex, 0, 0);


        //if (gravedad.gravedad == true)
        //{
        //    transform.GetChild(1).gameObject.SetActive(true);
        //    transform.GetChild(0).gameObject.SetActive(false);
        //}
        //else transform.GetChild(0).gameObject.SetActive(true); transform.GetChild(1).gameObject.SetActive(false);



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
        if (collision.gameObject.name == "batalla pieza 2.2 bake")
        {
            ground = true;
        }
    }

}
