using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MenuMusicEffectsNotifier : MonoBehaviour
{

    public static event Action GameStartedCallback;

    public void SendCallback()
    {
        GameStartedCallback?.Invoke();
    }

}
