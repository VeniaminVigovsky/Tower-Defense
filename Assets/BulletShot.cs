using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    private const float maxDistance = 0.3f;
    [SerializeField]
    LayerMask ignoreLayers;

    [SerializeField]
    LayerMask enemyLayer;

    RaycastHit hit;



    public delegate void OnHitHandler(Transform sender);
    public event OnHitHandler HitAny;



    private void FixedUpdate()
    {        

        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, ~ignoreLayers))
        {
            //check for IDAmagable

            if (hit.transform.GetComponentInParent<Enemy>() != null)
            {
                hit.transform.GetComponentInParent<Enemy>().TakeDamage(1);
            }
            else if(Physics.CheckSphere(transform.position, maxDistance, enemyLayer))
            {
                hit.transform.GetComponentInParent<Enemy>()?.TakeDamage(1);
            }

            HitAny?.Invoke(this.transform);           

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInParent<Enemy>() != null)
        {
            other.transform.GetComponentInParent<Enemy>().TakeDamage(1);
            
        }

        HitAny?.Invoke(this.transform);
    }

}
