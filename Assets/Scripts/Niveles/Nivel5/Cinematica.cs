using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematica : MonoBehaviour
{
    public GameObject cam;
    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Animacion());
    }

    private IEnumerator Animacion()
    {
        float startDensity = RenderSettings.fogDensity;
        float endDensity = 0.005f;
        float step = 0.001f;
        float waitTime = 0.1f;
        cam.SetActive(true);

        // Bajamos la densidad gradualmente
        for (float density = startDensity; density > endDensity; density -= step)
        {
            RenderSettings.fogDensity = density;
            yield return new WaitForSeconds(waitTime);
        }

        // Aseguramos que termina exactamente en el valor deseado
        RenderSettings.fogDensity = endDensity;
        yield return new WaitForSeconds(6f);

        cam.SetActive(false);
    }
}



