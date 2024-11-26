using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float MaxTimer = 60f;
    public float CurrentTimer;

    public void StartTimer() 
    {
        CurrentTimer = MaxTimer;
        while (CurrentTimer > 0)
        {
            DoTimer();
        }
        CurrentTimer = 0;

    }
    public void DoTimer() 
    {
        CurrentTimer -= Time.deltaTime;
    }
}
