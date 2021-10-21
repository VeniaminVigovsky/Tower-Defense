using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class EnemyAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _hitClip;
    
    private static double _lastHitTime;
    
    private double _timeBetweenHits;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _lastHitTime = -100;
        if (_hitClip != null)
            _timeBetweenHits = ((double)_hitClip.samples / _hitClip.frequency) * 0.1;
    }

    public void PlayHitSound()
    {
        if (_hitClip == null) return;

        if (AudioSettings.dspTime > _lastHitTime + _timeBetweenHits)
        {
            _audioSource.PlayOneShot(_hitClip);
            _lastHitTime = AudioSettings.dspTime;
        }
    }

}
