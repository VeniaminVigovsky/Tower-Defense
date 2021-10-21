using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicEffectManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;
    private AudioSource _audioSource;
    private float _fadeDuration = 3f;
    private AudioMixerSnapshot _LPsnapshot, _noLPsnapshot;
    private float _snapshotTransitionDuration = 0.1f;
    private float _musicVolumeMax = 0.85f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
        _audioSource?.DOFade(_musicVolumeMax, _fadeDuration).SetEase(Ease.Linear);
        
        _LPsnapshot = _audioMixer?.FindSnapshot("LPSnapshot");
        _noLPsnapshot = _audioMixer?.FindSnapshot("NOLPSnapshot");
        
        
    }

    private void Start()
    {
        _noLPsnapshot.TransitionTo(_snapshotTransitionDuration);
    }

    private void OnEnable()
    {
        Tower.TowerDestroyed += OnTowerDestroyedCallback;
    }

    private void OnDisable()
    {
        Tower.TowerDestroyed -= OnTowerDestroyedCallback;
    }

    private void OnTowerDestroyedCallback()
    {
        _LPsnapshot.TransitionTo(_snapshotTransitionDuration);

        _audioSource?.DOFade(0f, _fadeDuration);
    }


}
