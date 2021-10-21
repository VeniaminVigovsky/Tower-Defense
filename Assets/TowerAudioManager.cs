using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TowerAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _hitClip, _shockWaveClip;

    private static double _lastHitTime, _lastShockWaveTime;

    private double _timeBetweenHits, _timeBetweenShockWaves;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _lastHitTime = -100;
        _lastShockWaveTime = -100;
        if (_hitClip != null)
            _timeBetweenHits = ((double)_hitClip.samples / _hitClip.frequency) * 0.1;
        if (_shockWaveClip != null)
            _timeBetweenShockWaves = ((double)_shockWaveClip.samples / _shockWaveClip.frequency) * 0.1;
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

    public void PlayShockWaveSound()
    {
        if (_shockWaveClip == null) return;

        if (AudioSettings.dspTime > _lastShockWaveTime + _timeBetweenShockWaves)
        {
            _audioSource.PlayOneShot(_shockWaveClip);
            _lastShockWaveTime = AudioSettings.dspTime;
        }
    }
}
