using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : MonoBehaviour
{
    private int health;
    private int maxHealth;

    private Transform alive;
    private Transform destroyed;
    private TowerAudioManager _audioManager;

    public static event Action TowerDestroyed;

    private TowerDamageReceiver damageReceiver;
    public int Health
    {
        get
        {
            if (health < 0)
            {
                return 0;
            }
            else
            {
                return health;
            }
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
    }

    private void Awake()
    {
        alive = transform.Find("Alive");
        destroyed = transform.Find("Destroyed");
        damageReceiver = GetComponentInChildren<TowerDamageReceiver>();
        _audioManager = GetComponent<TowerAudioManager>();

        if (damageReceiver != null)
        {
            damageReceiver.ReceivedDamage += TakeDamage;
        }


        if (destroyed != null && alive != null)
        {
            alive.gameObject.SetActive(true);
            destroyed.gameObject.SetActive(false);
        }
        maxHealth = 10;
        health = maxHealth;
    }

    private void OnDisable()
    {
        damageReceiver.ReceivedDamage -= TakeDamage;
    }

    private void OnDestroy()
    {
        damageReceiver.ReceivedDamage -= TakeDamage;
    }


    public void TakeDamage()
    {

        health--;
        _audioManager?.PlayHitSound();
        if (health <= 0)
        {
            DestroyTower();
            TowerDestroyed?.Invoke();
        }
    }

    private void DestroyTower()
    {
        alive.gameObject.SetActive(false);
        destroyed.gameObject.SetActive(true);
        _audioManager?.PlayShockWaveSound();
    }
}
