using UnityEngine;
using UnityEngine.UI;


public class GameOverScreenHandler : MonoBehaviour
{
    private GameObject _endPanel;
    
    private Text _endPanelText;

    private Button[] _butons;

    private void Awake()
    {
        _endPanel = transform.Find("EndPanel").gameObject;
        _endPanel.SetActive(false);
        Tower.TowerDestroyed += ShowDefeatScreen;
        EnemySpawner.TimesUp += ShowVictoryScreen;
        _endPanelText = _endPanel.transform.GetComponentInChildren<Text>();
        _butons = _endPanel.transform.GetComponentsInChildren<Button>();
        if (_butons != null)
        {
            foreach (var button in _butons)
            {
                button.interactable = false;
            }
        }

    }

    private void SetButtonsActive()
    {
        if (_butons != null)
        {
            foreach (var button in _butons)
            {
                button.interactable = true;
            }
        }
    }

    private void OnDisable()
    {
        Tower.TowerDestroyed -= ShowDefeatScreen;
        EnemySpawner.TimesUp -= ShowVictoryScreen;
    }

    private void ShowDefeatScreen()
    {
        _endPanel.SetActive(true);
        _endPanelText.text = "YOU LOST!";
        EnemySpawner.TimesUp -= ShowVictoryScreen;
        Invoke("SetButtonsActive", 1f);
    }

    private void ShowVictoryScreen()
    {
        _endPanel.SetActive(true);
        _endPanelText.text = "VICTORY!";
        Tower.TowerDestroyed -= ShowDefeatScreen;
        Invoke("SetButtonsActive", 1f);
    }
}
