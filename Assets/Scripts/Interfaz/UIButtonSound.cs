using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;
            audioSource.PlayOneShot(hoverSound, audioSource.volume);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.volume = PlayerPrefs.GetFloat("efectos", 1f) * 1f;
            audioSource.PlayOneShot(clickSound, audioSource.volume);
        }
    }
}
    