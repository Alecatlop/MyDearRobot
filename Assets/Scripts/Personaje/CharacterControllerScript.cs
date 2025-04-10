using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
{
    Vector2 inputMove;
    Vector2 inputAim;
    CharacterController controller;
    float speed = 10f;
    public float gravity = -15f;
    public float jumpForce = 10f;
    public float verticalVelocity;
    bool isGrounded;
    bool pausar = false;
    public bool gravitycheck = true;

    public Gravedad gravedad;
    public Nivel1 accion1;
    public Nivel2 accion2;
    public GameMana nivel;
    public bool respawn = true;
    public bool altura = false;
    public Pausa pausa;
    private GameObject puerta;

    public bool spawn = false;

    public Animator animator;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        puerta = GameObject.Find("Puerta6");
    }

    void Update()
    {
        // Verificar si est� en el suelo
        isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Para mantener el personaje en el suelo
        }

        if(pausar == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

        }

        Transform cam = Camera.main.transform;

        // Direcciones horizontal y vertical de la c�mara
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // Eliminar inclinaci�n vertical
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();


        if(!spawn)
        {
            // Direcci�n final basada en input + c�mara
            Vector3 move = camRight * inputMove.x + camForward * inputMove.y;
            controller.Move(move * speed * Time.deltaTime);

            //animator.SetFloat("speed", move.magnitude);

            // Rotar el personaje si se est� moviendo
            if (move != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
            }

            float direction = gravitycheck ? 1f : -1f;
            verticalVelocity += gravity * direction * Time.deltaTime;
            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);

            if (verticalVelocity > 10f)
            {
                verticalVelocity = 10f;
            }

            if (verticalVelocity < -15f)
            {
                verticalVelocity = -15f;
            }

        }


    }

    private void OnMove(InputValue value)
    {
        inputMove = value.Get<Vector2>();
    }

    private void OnAim(InputValue value)
    {
        inputAim = value.Get<Vector2>();
    }

    private void OnPause()
    {
        pausar = !pausar;
        pausa.Pausar();
    }

    private void OnJump()
    {
        if (isGrounded && !pausar)
        {
            verticalVelocity = gravedad.gravedad ? -jumpForce : jumpForce;
            isGrounded = false;

            //animator.SetTrigger("jump");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("runa nivel1"))
        {
            other.GetComponent<Collider>().enabled = false;
            accion1.RunaColor();
        }

        if (other.CompareTag("Respawn"))
        {
            respawn = !respawn;
        }

        if (other.CompareTag("Finish"))
        {
            puerta.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Puerta2" && !isGrounded)
        {
            other.GetComponent<Collider>().enabled = false;
            accion2.ActivarPlataformas();
        }
    }
}
