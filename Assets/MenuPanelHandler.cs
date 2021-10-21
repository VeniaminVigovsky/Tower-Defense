using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuPanelHandler : MonoBehaviour
{
    private GameObject _menuPanel;
    private bool _isMenuLocked;

    private bool _isMenuUp;

   
    public static event Action GamePaused, GameUnpaused;
    private void Awake()
    {
        _menuPanel = transform.Find("MenuPanel").gameObject;
        _isMenuLocked = false;
        _isMenuUp = false;
        _menuPanel?.SetActive(false);
    }

    private void OnEnable()
    {
        Tower.TowerDestroyed += LockMenu;
        EnemySpawner.TimesUp += LockMenu;

        SceneSelectionManager.SceneTransitionStartedCallback += UnpauseGame;
    }

    private void OnDisable()
    {
        Tower.TowerDestroyed -= LockMenu;
        EnemySpawner.TimesUp -= LockMenu;

        SceneSelectionManager.SceneTransitionStartedCallback -= UnpauseGame;
    }



    public void ShowMenu()
    {
        if (!_isMenuLocked && _menuPanel != null && !_isMenuUp)
        {
            _menuPanel.SetActive(true);
            _isMenuUp = true;
            GamePaused?.Invoke();
            Time.timeScale = 0f;
        }
    }

    public void HideMenu()
    {
        if (!_isMenuLocked && _menuPanel != null && _isMenuUp)
        {
            _menuPanel.SetActive(false);
            _isMenuUp = false;
            GameUnpaused?.Invoke();
            Time.timeScale = 1f;
        }
    }

    private void LockMenu()
    {
        _isMenuLocked = true;
    }

    private void UnpauseGame()
    {
        GameUnpaused?.Invoke();
        Time.timeScale = 1f;
    }
}
