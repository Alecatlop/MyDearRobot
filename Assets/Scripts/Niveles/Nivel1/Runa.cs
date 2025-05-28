using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runa : MonoBehaviour
{
    public GameObject runa;
    public GameObject camara;
    public bool activado = false;
    public AudioSource audioSource;
    public AudioClip sonidoActivacion;

    private IEnumerator Pintar()
    {
        activado = true;

        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.yellow * 2);
        camara.SetActive(true);

        yield return new WaitForSeconds(2f);
        Material runaMat = runa.GetComponent<MeshRenderer>().material;
        runaMat.EnableKeyword("_EMISSION");
        runaMat.SetColor("_EmissionColor", Color.yellow * 2);

        if (audioSource != null && sonidoActivacion != null)
        {
            audioSource.PlayOneShot(sonidoActivacion);
        }

        yield return new WaitForSeconds(2f);
        camara.SetActive(false);

        yield return null; // Opcional, por si en el futuro quieres a�adir un tiempo o animaci�n

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Jugador")
        {
            StartCoroutine(Pintar());
        }
    }
}
