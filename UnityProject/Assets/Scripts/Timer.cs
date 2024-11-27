using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Это я баловался
    // Потом доделаю
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
        CurrentTimer -= Time.deltaTime; // Time.deltaTime - время между сменой кадров в секундах
    }
}
