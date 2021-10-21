using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class MenuMusicEffectsManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;

    private AudioSource _audioSource;

    private AudioMixerSnapshot _LPsnapshot, _noLPsnapshot;
    private float _snapshotTransitionDuration = 0.1f;
    private float _fadeDuration = 3f;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _LPsnapshot = _audioMixer?.FindSnapshot("LPSnapshot");
        _noLPsnapshot = _audioMixer?.FindSnapshot("NOLPSnapshot");
    }

    private void OnEnable()
    {
        MenuMusicEffectsNotifier.GameStartedCallback += FadeOutWithTransitionToLP;
    }

    private void OnDisable()
    {
        MenuMusicEffectsNotifier.GameStartedCallback -= FadeOutWithTransitionToLP;        
    }

    private void Start()
    {
        _noLPsnapshot.TransitionTo(0f);
    }

    private void FadeOutWithTransitionToLP()
    {
        _LPsnapshot.TransitionTo(_snapshotTransitionDuration);
        //_audioSource?.DOFade(0f,_fadeDuration);
    }
}
