using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class WeaponAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _shotClip, _hitClip;
    private AudioSource _audioSource;

    private double _lastShotTime;
    private double _lastHitTime;

    private double _timeBetweenShots;
    private double _timeBetweenHits;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;

        if (_hitClip != null)
            _timeBetweenHits = ((double)_hitClip.samples / _hitClip.frequency) * 0.1;
        if (_shotClip != null)
            _timeBetweenShots = ((double)_shotClip.samples / _shotClip.frequency) * 0.1;
    }

    public void PlayShotSound()
    {
        if (_shotClip == null) return;

        if (AudioSettings.dspTime > _lastShotTime + _timeBetweenShots)
        {
            _audioSource.PlayOneShot(_shotClip);
            _lastShotTime = AudioSettings.dspTime;
        }
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
