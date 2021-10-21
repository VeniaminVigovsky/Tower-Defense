using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static float _roundTime;

    

    private static float _timeSinceLevelStarted;

    private void Awake()
    {
        _timeSinceLevelStarted = Time.time;
        //Cursor.visible = false;


    }

    //private void OnEnable()
    //{
    //    MenuPanelHandler.GamePaused += ShowCursor;
    //    MenuPanelHandler.GameUnpaused += HideCursor;

    //    Tower.TowerDestroyed += ShowCursor;
    //    EnemySpawner.TimesUp += ShowCursor;

    //    SceneLoader.SceneChanged += ShowCursor;
    //}

    //private void OnDisable()
    //{
    //    MenuPanelHandler.GamePaused -= ShowCursor;
    //    MenuPanelHandler.GameUnpaused -= HideCursor;

    //    Tower.TowerDestroyed -= ShowCursor;
    //    EnemySpawner.TimesUp -= ShowCursor;

    //    SceneLoader.SceneChanged -= ShowCursor;
    //}

    //private void HideCursor() => Cursor.visible = false;

    //private void ShowCursor() => Cursor.visible = true;

    public static float ElapsedTime
    {
        get 
        {
           return Time.time - _timeSinceLevelStarted;
        }
    }


    public static float RoundTime
    {
        get => _roundTime;
    }

    public static void SetRoundTimeOffAudio(double l)
    {
        _roundTime = (float)l;
    }
}
