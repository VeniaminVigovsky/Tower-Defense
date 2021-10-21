using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
    private Vector2 positionOnScreen;
    private Vector2 mousePosition;
    [SerializeField]
    private Transform tower;
    private Quaternion minRot, maxRot;
    [SerializeField]
    private float minX = 20f, maxX = -40f;
    private void Start()
    {
        if (tower != null)
        {
            positionOnScreen = Camera.main.WorldToViewportPoint(tower.position);
            
        }

        Vector3 minRotation = new Vector3(minX, 0f, 0f);
        Vector3 maxRotation = new Vector3(maxX, 0f, 0f);
        minRot = Quaternion.Euler(new Vector3(minRotation.x, 0f, 0f));
        maxRot = Quaternion.Euler(new Vector3(maxRotation.x, 0f, 0f));

    }
    // Update is called once per frame
    private void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePosition, positionOnScreen);
        float invL = Mathf.InverseLerp(0.1f, 0.45f, distance);
        Quaternion angle = Quaternion.Slerp(minRot, maxRot, invL);
        angle = Quaternion.Euler(new Vector3(angle.eulerAngles.x, 0f, 0f));
        transform.localEulerAngles = new Vector3(angle.eulerAngles.x, 0f, 0f);
        
        //Debug.Log($"distance {distance}, invL {invL}");
    }
}
