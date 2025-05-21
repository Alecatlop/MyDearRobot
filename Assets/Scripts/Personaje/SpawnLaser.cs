using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class SpawnLaser : MonoBehaviour
{
    public GameObject spawn;
    public GameObject jugador;
    public GameObject canvas;
    public UnityEngine.UI.Image fadeImage;
    public CharacterControllerScript velocidad;


    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        velocidad = jugador.GetComponent<CharacterControllerScript>();
        canvas = GameObject.Find("Fade");
        fadeImage = canvas.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Caida());
        }
    }

    public IEnumerator Caida()
    {
        // Fade In (oscurecer)
        yield return StartCoroutine(FadeIn(0f, 1f, 1f)); // De transparente a negro en 1 segundo     

        Debug.Log("Respawneo");

        // Espera un poquito antes de hacer el fade out
        yield return new WaitForSeconds(0.5f);

        // Fade Out (mostrar juego)
        yield return StartCoroutine(FadeOut(1f, 0f, 1f)); // De negro a transparente en 1 segundo

    }

    private IEnumerator FadeIn (float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Asegura que termina exactamente con el alpha deseado
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);

        velocidad.spawn = true;

        velocidad.AñadirMuerte();

        CharacterController controller = jugador.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;  // Desactiva el CharacterController
            jugador.transform.position = spawn.transform.position;  // Mueves el jugador
            controller.enabled = true;   // Se vuelve a activar el CharacterController
        }
        else
        {
            jugador.transform.position = spawn.transform.position;  // Por si no tiene controller
        }
    }

    private IEnumerator FadeOut(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Asegura que termina exactamente con el alpha deseado
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);

        velocidad.spawn = false;

    }
}
