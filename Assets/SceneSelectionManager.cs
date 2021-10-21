using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectionManager : MonoBehaviour
{

    public static event Action SceneTransitionStartedCallback;

    public void LoadMenuScene()
    {
        
        SceneLoader.Instance?.LoadScene(1);
        SceneTransitionStartedCallback?.Invoke();
    }

    public void LoadGameScene(int level)
    {
        

        if (level < SceneManager.sceneCountInBuildSettings) 
        {


            SceneLoader.Instance?.LoadScene(level);
            SceneTransitionStartedCallback?.Invoke();

        }

            
    }

    public void ReloadGameScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;


        SceneLoader.Instance?.LoadScene(sceneIndex);
        SceneTransitionStartedCallback?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
