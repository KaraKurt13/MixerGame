using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private AudioClip clickSound;
    private AudioSource buttonAudioSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonAudioSource.PlayOneShot(clickSound);
    }

    void Start()
    {
        buttonAudioSource = GetComponent<AudioSource>();
    }

    
}
