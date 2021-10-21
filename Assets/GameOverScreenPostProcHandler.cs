using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOverScreenPostProcHandler : MonoBehaviour
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
            EnemySpawner.TimesUp += SwitchGreyScreenOn;
            Tower.TowerDestroyed += SwitchGreyScreenOn;

        }
            
    }
    private void OnDisable()
    {
        EnemySpawner.TimesUp -= SwitchGreyScreenOn;
        Tower.TowerDestroyed -= SwitchGreyScreenOn;
    }


    private void SwitchGreyScreenOn()
    {
        if (hasColAdj)
            colorAdjustments.active = true;
    }
}
