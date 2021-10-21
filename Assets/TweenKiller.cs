using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenKiller : MonoBehaviour
{

    private static TweenKiller Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        DOTween.Clear();
    }

    private void OnDisable()
    {
        DOTween.Clear();
    }
}
