using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    private BulletShot _bulletShot;
    private Rigidbody _rigidbody;
    private BulletSpawner _bulletSpawner;
    
    
    private float _speed;
    


    private void Awake()
    {


        _bulletShot = GetComponentInChildren<BulletShot>();
        _bulletSpawner = GetComponentInParent<BulletSpawner>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _speed = 1500f;
        
    }



    private void Update()
    {
        if (Vector3.Distance(transform.position, _bulletShot.transform.position) > 100f)
        {
            ReturnToPool(transform);
        }
    }

    private void OnEnable()
    {        
        _bulletShot.HitAny += ReturnToPool;        
        _rigidbody.AddForce(transform.forward * _speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void ReturnToPool(Transform sender)
    {
        _bulletSpawner.BurstParticles(sender);
        _rigidbody.velocity = Vector3.zero;
        _bulletShot.transform.position = transform.position;
        
        transform.gameObject.SetActive(false);
        
    }





    private void OnDisable()
    {        
        _bulletShot.HitAny -= ReturnToPool;        
    }







}
