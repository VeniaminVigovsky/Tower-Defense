using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettingsManager
{
    private static float _musicGain = 1f, _sfxGain = 1f;

    private const float _minAudioGainValue = 0.0001f;

    public static float MusicGain
    {
        get => _musicGain;

        set
        {
            value = Mathf.Max(value, _minAudioGainValue);
            _musicGain = value;
        }
    }

    public static float SFXGain
    {
        get => _sfxGain;

        set
        {
            value = Mathf.Max(value, _minAudioGainValue);
            _sfxGain = value;
        }
    }


}
