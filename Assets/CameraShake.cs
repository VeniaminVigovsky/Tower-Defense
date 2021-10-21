using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private TowerDamageReceiver damageReceiver;
    private float shakeSpeed;
    Vector3 initialPosition;
    int shakeCount;

    private void Awake()
    {
        if (damageReceiver != null)
        {
            damageReceiver.ReceivedDamage += ShakeCamera;
        }

        shakeSpeed = 0.06f;
        initialPosition = transform.position;
        shakeCount = 0;
    }

    private void OnDestroy()
    {
        damageReceiver.ReceivedDamage -= ShakeCamera;
    }

    private void OnDisable()
    {
        damageReceiver.ReceivedDamage -= ShakeCamera;
    }

    private void ShakeCamera()
    {
        if (shakeCount < 4)
        {
            Vector3 offset = Random.insideUnitCircle;

            transform.DOLocalMove(initialPosition + offset, shakeSpeed).OnComplete(ShakeCamera);
            shakeCount++;
        }
        else
        {
            transform.DOLocalMove(initialPosition, shakeSpeed);
            shakeCount = 0;
        }


    }
}
