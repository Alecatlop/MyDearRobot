using System.Collections;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float desplazamientoTiempo = 1f;
    [SerializeField] private float tiempoEntreMovimientos = 3f;

    public AudioClip sonidoLaser;
    private AudioSource audioSource;

    void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(MovimientoLaser());
    }

    private IEnumerator MovimientoLaser()
    {
        float delayInicial = Random.Range(1f, 5f);
        yield return new WaitForSeconds(delayInicial);

        while (true)
        {
            ReproducirSonido();
            yield return Mover(Vector3.down);
            yield return new WaitForSeconds(tiempoEntreMovimientos);

            ReproducirSonido();
            yield return Mover(Vector3.up);
            yield return new WaitForSeconds(tiempoEntreMovimientos);
        }
    }

    private IEnumerator Mover(Vector3 direccion)
    {
        float tiempo = 0f;
        while (tiempo < desplazamientoTiempo)
        {
            transform.Translate(direccion * speed * Time.deltaTime);
            tiempo += Time.deltaTime;
            yield return null;
        }
    }

    private void ReproducirSonido()
    {
        if (sonidoLaser != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoLaser);
        }
    }
}
