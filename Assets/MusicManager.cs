using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _mainPart, _ending;

    private double _mainClipLength, _startTime, _mainClipRemainedLength;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        if (_ending != null && _mainPart != null)
        {
            _mainClipLength = (double)_mainPart.samples / _mainPart.frequency;
            GameManager.SetRoundTimeOffAudio(_mainClipLength * 0.99);
            _audioSource.clip = _ending;
            _audioSource.PlayOneShot(_mainPart);
            _startTime = AudioSettings.dspTime;
            _mainClipRemainedLength = _mainClipLength;
            _audioSource.PlayScheduled(_startTime + _mainClipRemainedLength);
        }

    }


    private void OnEnable()
    {
        MenuPanelHandler.GamePaused += PauseMusic;
        MenuPanelHandler.GameUnpaused += UnpauseMusic;
    }

    private void OnDisable()
    {
        MenuPanelHandler.GamePaused -= PauseMusic;
        MenuPanelHandler.GameUnpaused -= UnpauseMusic;
    }

    private void PauseMusic()
    {
        _audioSource.Pause();
        _mainClipRemainedLength -=(AudioSettings.dspTime - _startTime);
        
                
    }

    private void UnpauseMusic()
    {
        _audioSource.UnPause();
        _startTime = AudioSettings.dspTime;
        _audioSource.SetScheduledStartTime(_startTime + _mainClipRemainedLength);
    }

}
