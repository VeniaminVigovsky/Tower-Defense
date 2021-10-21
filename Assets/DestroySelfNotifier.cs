using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfNotifier : MonoBehaviour
{    
    private Enemy enemy;
    private void Awake()
    {
        enemy = transform.GetComponentInParent<Enemy>();
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AlivePlayer"))
        {
            enemy?.OnTowerReached(other);

        }

    }
}
