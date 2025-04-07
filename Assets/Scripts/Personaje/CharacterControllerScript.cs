using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
{
    Vector2 inputMove;
    Vector2 inputAim;
    CharacterController controller;
    float speed = 7f;
    float gravity = -9.81f;
    float jumpForce = 10f;
    float verticalVelocity;
    float anglex;
    Vector2 sensibilidad = new Vector2(35, 15);
    bool isGrounded;
    bool pausar = false;

    public Gravedad gravedad;
    public Nivel1 accion1;
    public Nivel2 accion2;
    public GameMana nivel;
    public bool respawn = true;
    public bool altura = false;
    public Pausa pausa;
    private GameObject puerta;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        puerta = GameObject.Find("Puerta6");
    }

    void Update()
    {
        // Verificar si está en el suelo
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

        // Direcciones horizontal y vertical de la cámara
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // Eliminar inclinación vertical
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Dirección final basada en input + cámara
        Vector3 move = camRight * inputMove.x + camForward * inputMove.y;
        controller.Move(move * speed * Time.deltaTime);

        // Rotar el personaje si se está moviendo
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
        }

        // Aplicar gravedad
        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        Caida();
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
        }
    }

    private void Caida()
    {
        if (Mathf.Abs(verticalVelocity) > 12f)
        {
            altura = true;
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
