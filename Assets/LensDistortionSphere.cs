using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LensDistortionSphere : MonoBehaviour
{
    private const int scaleMultiplier = 300;
    private Vector3 initScale;
    private float scaleDuration = 0.4f;

    private void OnEnable()
    {
        initScale = transform.localScale;
        Tower.TowerDestroyed += ScaleSphere;
        
    }

    private void OnDisable()
    {
        Tower.TowerDestroyed -= ScaleSphere;
    }

    private void OnDestroy()
    {
        Tower.TowerDestroyed -= ScaleSphere;
    }

    private void ScaleSphere()
    {
        transform.DOScale(initScale * scaleMultiplier, scaleDuration).SetEase(Ease.InFlash).
            OnComplete(() => transform.DOScale(initScale, 0f));
    }

}
