using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatControl : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletUIprefab, _overHeatFrame;

    private Animator _overHeatAnimator;

    private Image _bulletSprite;
    private Image _heatLevel;
    

    

    [SerializeField]
    private BulletSpawner bulletSpawner;
    private void Awake()
    {
        if (_bulletUIprefab != null)
        {
            _bulletSprite = _bulletUIprefab.transform.Find("BulletSprite").GetComponent<Image>();
            _heatLevel = _bulletUIprefab.transform.Find("HeatLevel").GetComponent<Image>();
        }

        _overHeatAnimator = _overHeatFrame.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        bulletSpawner.Overheated += OnOverheated;
        bulletSpawner.CoolDowned += OnCoolDowned;

    }

    private void OnDisable()
    {
        bulletSpawner.Overheated -= OnOverheated;
        bulletSpawner.CoolDowned -= OnCoolDowned;
    }

    private void Update()
    {
        _heatLevel.fillAmount = bulletSpawner.CurrentHeatLevel;
    }



    private void OnOverheated()
    {
        _overHeatAnimator.SetTrigger("Overheat");
    }

    private void OnCoolDowned()
    {
        _overHeatAnimator.SetTrigger("Cooldown");
    }


}
