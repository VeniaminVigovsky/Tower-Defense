using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MenuScreenPostProcessing : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private bool hasColAdj;
    private void Awake()
    {
        volume = GetComponent<Volume>();
        hasColAdj = volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        if (hasColAdj)
        {
            colorAdjustments.active = false;
            MenuPanelHandler.GamePaused += SwitchGreyScreenOn;
            MenuPanelHandler.GameUnpaused += SwitchGreyScreenOff;

        }

    }
    private void OnDisable()
    {
        MenuPanelHandler.GamePaused -= SwitchGreyScreenOn;
        MenuPanelHandler.GameUnpaused -= SwitchGreyScreenOff;
    }


    private void SwitchGreyScreenOn()
    {
        if (hasColAdj)
            colorAdjustments.active = true;
    }

    private void SwitchGreyScreenOff()
    {
        if (hasColAdj)
            colorAdjustments.active = false;
    }
}
