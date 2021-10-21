using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected AStar aStar;    
    protected NodeGrid grid;    
    protected Tower tower;

    protected EnemyAudioManager _audioManager;

    protected Transform graphics;
    protected Transform burst;

    public int id;

    public virtual void EnemyConstructor(NodeGrid grid, Tower tower, int id)
    {
        this.grid = grid;
        this.tower = tower;
        aStar = new AStar(grid);

        this.id = id;
        graphics = transform.Find("Graphics");
        burst = transform.Find("Burst");

        if (burst != null && graphics != null)
        {
            burst.gameObject.SetActive(false);
            graphics.gameObject.SetActive(true);
        }

        _audioManager = GetComponent<EnemyAudioManager>();
    }

    public virtual void TakeDamage(int damage)
    {

        DestroySelf();

    }

    public virtual void DestroySelf()
    {

        _audioManager?.PlayHitSound();
        StopAllCoroutines();
        if (graphics != null && burst != null)
        {
            graphics.gameObject.SetActive(false);
            burst.gameObject.SetActive(true);
        }

        StartCoroutine(DisableDelayed());
    }

    private IEnumerator DisableDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        graphics.gameObject.SetActive(true);
        burst.gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
    }

    public virtual void OnTowerReached(Collider other)
    {
        TowerDamageReceiver damageReceiver = other.GetComponent<TowerDamageReceiver>();

        damageReceiver?.ReceiveDamage();

        DestroySelf();
    }



    
}
