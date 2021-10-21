using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerDamageReceiver : MonoBehaviour
{

    public event Action ReceivedDamage;

    public void ReceiveDamage()
    {
        ReceivedDamage?.Invoke();
    }

}
