using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTime : MonoBehaviour
{

    float t = 0, d = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {


            t += Time.time;
            d += Time.deltaTime;
            Debug.Log($"t: {t}");
            Debug.Log($"d: {d}");
        }
    }
}
