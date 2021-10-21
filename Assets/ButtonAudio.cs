using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _onPointerEnterSound, _onPointerClickSound;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_onPointerClickSound != null)
            _audioSource?.PlayOneShot(_onPointerClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_onPointerEnterSound != null)
            _audioSource?.PlayOneShot(_onPointerEnterSound);
    }
}
