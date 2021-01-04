using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMane : MonoBehaviour
{
    private float Now_Time;
    public int Mse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Now_Time += Time.fixedDeltaTime;
        Mse = (int)(Now_Time * 1000);
    }
}
