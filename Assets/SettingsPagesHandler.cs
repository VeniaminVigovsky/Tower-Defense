using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPagesHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _settingsPanels;
    [SerializeField]
    private GameObject _mainMenuPanel;

    private GameObject _currentPanel;

    private void Awake()
    {        
        _currentPanel = _mainMenuPanel;

        if (_settingsPanels != null)
        {            
            for (int i = 0; i < _settingsPanels.Length; i++)
            {
                _settingsPanels[i]?.SetActive(false);
            }            
        }

    }

    public void OpenPanel(int id)
    {
        if (id >= _settingsPanels.Length) return;

        _currentPanel?.SetActive(false);
        _currentPanel = _settingsPanels[id];
        _currentPanel?.SetActive(true);

    }

    public void OpenMenuPanel()
    {
        _currentPanel?.SetActive(false);        
        _currentPanel = _mainMenuPanel;
        _currentPanel?.SetActive(true);
    }

}
