using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;


public class AudioSettingsMenuHandler : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;

    private void Awake()
    {
        SetAudioValues();
    }

    public void SetMusicGain(float sliderValue)
    {
        _audioMixer.SetFloat("MusicGain", Mathf.Log10(sliderValue) * 20);
        GameSettingsManager.MusicGain = sliderValue;
    }

    public void SetSFXGain(float sliderValue)
    {
        _audioMixer.SetFloat("SFXGain", Mathf.Log10(sliderValue) * 20);
        GameSettingsManager.SFXGain = sliderValue;
    }

    public void SetDefault()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();

        if (sliders != null)
        {
            foreach (var slider in sliders)
            {
                slider.value = 1;
            }
        } 
        GameSettingsManager.MusicGain = 1;
        GameSettingsManager.SFXGain = 1;
    }

    private void SetAudioValues()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();

        sliders.Where((x) => x.gameObject.name == "MusicSlider").First().value = GameSettingsManager.MusicGain;
        sliders.Where((x) => x.gameObject.name == "SFXSlider").First().value = GameSettingsManager.SFXGain;
    }

}
