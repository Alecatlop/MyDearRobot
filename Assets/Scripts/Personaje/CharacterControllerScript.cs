using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterControllerScript : MonoBehaviour
{
    Vector2 inputMove;
    Vector2 inputAim;
    CharacterController controller;
    float speed = 8f;
    public float gravity = -15f;
    public float jumpForce = 10f;
    public float verticalVelocity;
    bool isGrounded;
    bool estabaEnElSuelo;
    public bool pausar = false;
    public bool gravitycheck = true;
    public Nivel1 accion1;
    public Nivel2 accion2;
    public GameMana nivel;
    public bool respawn = true;
    public bool altura = false;
    public Pausa pausa;
    private GameObject puerta;
    GameObject jefe;

    public bool spawn = false;

    public Animator animator;

    public Renderer[] partesDelRobot;
    public Material[] materialesNormales;
    public Material[] materialesDañados;
    public Material[] materialesMuyDañados;

    public int muertesParaDaño = 3;
    public int muertesParaMuyDaño = 6;
    public int muertesActuales = 0;

    Transform plataformaActual = null;
    Vector3 ultimaPosicionPlataforma;
    bool sobrePlataforma = false;

    public AudioSource pasosSource;
    public AudioSource efectosSource;
    public AudioClip clipPaso;
    public AudioClip clipSalto;
    public AudioClip clipAterrizaje;

    private float pasoTiempo = 0f;
    public float tiempoEntrePaso = 0.5f;
    public float velocidadCaidaMinimaSonido = -6f; 
    private float ultimaVelocidadAntesDeTocarSuelo = 0f; 

    private float tiempoMoviendo = 0f;
    private const float tiempoMinimoParaPaso = 0.15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        puerta = GameObject.Find("Puerta6");

        jefe = GameObject.Find("Modelo Jefe");
    }

    void Update()
    {
        // Verificar si est� en el suelo
        isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, 0.2f);

        if (!isGrounded)
        {
            ultimaVelocidadAntesDeTocarSuelo = verticalVelocity; 
        }

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        animator.SetBool("falling", !isGrounded && verticalVelocity < 0f);

        Cursor.lockState = pausar ? CursorLockMode.None : CursorLockMode.Locked;

        Transform cam = Camera.main.transform;
        // Direcciones horizontal y vertical de la c�mara
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        // Eliminar inclinaci�n vertical
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        if (!spawn)
        {
            Vector3 move = camRight * inputMove.x + camForward * inputMove.y;
            controller.Move(move * speed * Time.deltaTime);

            if (isGrounded)
            {
                animator.SetFloat("speed", move.magnitude);

                 // Sonido sincronizado con los pasos
                if (inputMove.magnitude > 0.1f)
                {
                    tiempoMoviendo += Time.deltaTime;
                    pasoTiempo -= Time.deltaTime;

                    if (tiempoMoviendo >= tiempoMinimoParaPaso && pasoTiempo <= 0f)
                    {
                        pasosSource.PlayOneShot(clipPaso);
                        pasoTiempo = tiempoEntrePaso;
                    }
                }
                else
                {
                    tiempoMoviendo = 0f;
                    pasoTiempo = 0f;
                }
            }
            else
            {
                animator.SetFloat("speed", 0f);
            }

            // Rotar el personaje si se est� moviendo
            if (move != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.deltaTime);
            }

            float direction = gravitycheck ? 1f : -1f;
            verticalVelocity += gravity * direction * Time.deltaTime;
            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);

            verticalVelocity = Mathf.Clamp(verticalVelocity, -15f, 10f);
        }

        // Aterrizaje
        if (!estabaEnElSuelo && isGrounded && ultimaVelocidadAntesDeTocarSuelo <= velocidadCaidaMinimaSonido) 
        {
            efectosSource.PlayOneShot(clipAterrizaje);
        }

        estabaEnElSuelo = isGrounded;

        // Si el jugador está sobre una plataforma, se mueve con ella
        if (plataformaActual != null && sobrePlataforma)
        {
            Vector3 movimientoPlataforma = plataformaActual.position - ultimaPosicionPlataforma;
            controller.Move(movimientoPlataforma);
            ultimaPosicionPlataforma = plataformaActual.position;
        }

        // Si el jugador no toca el suelo no estamos en ninguna plataforma
        if (!isGrounded)
        {
            sobrePlataforma = false;
            plataformaActual = null;
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
        pausa.Pausar();
    }

    private void OnJump()
    {
        if (isGrounded && !pausar)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
            animator.SetTrigger("jump");

            efectosSource.PlayOneShot(clipSalto); 

            plataformaActual = null;
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Plataforma"))
        {
            if (plataformaActual != hit.transform)
            {
                plataformaActual = hit.transform;
                ultimaPosicionPlataforma = plataformaActual.position;
            }

            sobrePlataforma = true;
        }

        if (hit.gameObject.name == "Jefe")
        {
            print("activar muerte hit");
            AñadirMuerte();
        }
    }

    public void AñadirMuerte()
    {
        muertesActuales++;
        Debug.Log("Muertes: " + muertesActuales);

        if (muertesActuales >= muertesParaMuyDaño)
        {
            CambiarMateriales(materialesMuyDañados);
        }
        else if (muertesActuales >= muertesParaDaño)
        {
            CambiarMateriales(materialesDañados);
        }
    }

    private void CambiarMateriales(Material[] nuevosMateriales)
    {
        if (nuevosMateriales.Length != partesDelRobot.Length)
        {
            Debug.Log("No coinciden la cantidad de materiales con las partes del robot");
            return;
        }

        for (int i = 0; i < partesDelRobot.Length; i++)
        {
            partesDelRobot[i].material = nuevosMateriales[i];
        }
    }
}
