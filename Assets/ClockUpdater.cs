using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUpdater : MonoBehaviour
{
    
    private Image _clock;

    private float _roundTime;
    private bool _stopUpdateTime;

   
    private void Awake()
    {
        _clock = GetComponentsInChildren<Image>()[1];        
        _roundTime = GameManager.RoundTime;
        _stopUpdateTime = false;
    }

    private void OnEnable()
    {
        Tower.TowerDestroyed += StopUpdateTime;
    }

    private void OnDisable()
    {
        Tower.TowerDestroyed -= StopUpdateTime;
    }

    private void StopUpdateTime()
    {
        _stopUpdateTime = true;
    }

    private void Update()
    {

        if (_stopUpdateTime) return;

        float tElapsed = Mathf.Clamp(GameManager.ElapsedTime, 0, _roundTime);
        _clock.fillAmount = 1 - tElapsed / _roundTime;
    }
}
