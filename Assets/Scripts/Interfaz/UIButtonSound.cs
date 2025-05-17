using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public float volumenBase = 1f;

    void Start()
    {
        ActualizarVolumen();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound, audioSource.volume);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound, audioSource.volume);
        }
    }

    public void ActualizarVolumen()
    {
        float volumenGlobal = PlayerPrefs.GetFloat("efectos", 1f);
        if (audioSource != null)
            audioSource.volume = volumenGlobal * volumenBase;
    }
}
