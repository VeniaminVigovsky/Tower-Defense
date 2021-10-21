using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab, particlesPrefab;
    private WeaponAudioManager _weaponAudio;


    [SerializeField]
    private int bulletPoolSize = 14;

    private int _particlesPoolSize;

    private List<GameObject> _bulletPool;

    private List<GameObject> _particlePool;

    private float _coolDownTime;
    private float _lastShotTime;
    private float _overHeatTime;

    private IWeapon _currentWeapon;

    private float _currentWeaponHeatCapacity;
    private float _currentHeatLevel;
    [SerializeField]
    private float _currentHeatUpSpeed;
    private float _currentOverHeatCoolDownTime;
    private bool _overHeated;
    private bool _cooldownPaused;    
    private float _cooldownSpeed;
    private float _deltaTimeCoof;

    public event Action Overheated;
    public event Action CoolDowned;

    public float CurrentHeatLevel
    {
        get => _currentHeatLevel /_currentWeaponHeatCapacity;
    }

    private void Awake()
    {
        _bulletPool = new List<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject b = Instantiate(bulletPrefab, transform);
            _bulletPool.Add(b);
            b.SetActive(false);
        }


        _particlePool = new List<GameObject>();

        _particlesPoolSize = bulletPoolSize;

        for (int i = 0; i < _particlesPoolSize; i++)
        {
            GameObject p = Instantiate(particlesPrefab, transform);
            p.SetActive(false);
            _particlePool.Add(p);
        }

        _lastShotTime = -100f;
        _overHeatTime = -100f;        
        _cooldownSpeed = 2.2f;
        _coolDownTime = 0.3f;
        _currentWeaponHeatCapacity = 10f;
        _currentHeatLevel = 0f;
        _currentOverHeatCoolDownTime = 1.4f;
        _currentHeatUpSpeed = _currentHeatUpSpeed > 0.01f ? _currentHeatUpSpeed : 1.25f;
               
        
        _weaponAudio = GetComponent<WeaponAudioManager>();
    }

    private void OnEnable()
    {
        MenuPanelHandler.GamePaused += PauseCooldown;
        MenuPanelHandler.GameUnpaused += UnpauseCooldown;
    }

    private void OnDisable()
    {
        MenuPanelHandler.GamePaused -= PauseCooldown;
        MenuPanelHandler.GameUnpaused -= UnpauseCooldown;
    }

    private void PauseCooldown()
    {
        _cooldownPaused = true;
    }

    private void UnpauseCooldown()
    {
        _cooldownPaused = false;
    }



    private void Update()
    {
        

        if (Input.GetMouseButton(0) && !_overHeated && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Time.time > _lastShotTime + _coolDownTime)
            {
                SpawnBullets();
                _lastShotTime = Time.time;
                _currentHeatLevel += (_currentHeatUpSpeed * Time.deltaTime);
                _weaponAudio.PlayShotSound();

                if (_currentHeatLevel >= _currentWeaponHeatCapacity)
                {
                    _overHeated = true;
                    _overHeatTime = Time.time;
                    Overheated?.Invoke();
                }

            }
            
        }

        if (!_cooldownPaused)
        {
            _currentHeatLevel -= (_cooldownSpeed * Time.deltaTime);
            _currentHeatLevel = Mathf.Clamp(_currentHeatLevel, 0, _currentWeaponHeatCapacity);
        }



        if (_overHeated && Time.time > _overHeatTime + _currentOverHeatCoolDownTime)
        {            
            _overHeated = false;
            CoolDowned?.Invoke();
        }

    }

    private void SpawnBullets()
    {
        GameObject bullet = GetBullet();
        
        bullet.SetActive(true);
    }

    private GameObject GetBullet()
    {
        foreach (var b in _bulletPool)
        {
            if (!b.activeInHierarchy)
            {
                return b;
            }
        }
        GameObject g = Instantiate(bulletPrefab, transform);
        _bulletPool.Add(g);
        g.SetActive(false);

        return g;
    }

    public GameObject GetParticles()
    {
        foreach (var particle in _particlePool)
        {
            if (!particle.activeInHierarchy)
            {
                return particle;
            }
        }

        GameObject p = Instantiate(particlesPrefab, transform);
        p.SetActive(false);

        return p;

    }


    public void BurstParticles(Transform sender)
    {
        _weaponAudio.PlayHitSound();
        GameObject p = GetParticles();
        p.transform.position = sender.position;
        p.SetActive(true); 
        StartCoroutine(DisableParticles(p));
    }

    private IEnumerator DisableParticles(GameObject p)
    {
        yield return new WaitForSeconds(0.3f);
        p.SetActive(false);
    }
}
