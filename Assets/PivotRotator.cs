using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotator : MonoBehaviour
{
    [SerializeField]
    LayerMask floorLayer;

    [SerializeField]
    LayerMask layersToIgnore;
    private float rotationSpeed = 100f;

    

    // Update is called once per frame
    private void FixedUpdate()
    {
        RotatePivot();
    }



    private void RotatePivot()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layersToIgnore))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z), Vector3.up);

        }
    }




}
