using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class MenuMusicManager : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _introFirst, _introLoop;
    private double _introFirstLength;
    private float _maxMusicVolume;
    private float _fadeDuration = 3f;    



    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.volume = 0f;
        _maxMusicVolume = 0.85f;
        if (_introFirst != null && _introLoop != null)
        {                       
            _audioSource.clip = _introLoop;
            _introFirstLength = (double)_introFirst.samples / _introFirst.frequency;
            _audioSource.PlayOneShot(_introFirst);
            _audioSource.PlayScheduled(AudioSettings.dspTime + _introFirstLength);
        }
        
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0;

        while(_audioSource.volume < _maxMusicVolume - 0.01f)
        {

            float value = Mathf.Lerp(0, _maxMusicVolume, t);

            _audioSource.volume = value;            

            t += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


}
