using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField]
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader?.LoadScene(1);
    }


}
