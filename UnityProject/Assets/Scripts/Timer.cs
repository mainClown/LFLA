using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Это я баловался
    // Потом доделаю
    // P.S. Походу не успею сделать, UI всю ману высосал
    public float MaxTimer = 60f;
    public float CurrentTimer;
    // Простенький алгоритм таймера, не воспринимать всерьёз
    public void StartTimer() 
    {
        CurrentTimer = MaxTimer;
        while (CurrentTimer > 0)
        {
            CurrentTimer -= Time.deltaTime; // Time.deltaTime - время между сменой кадров в секундах
        }
        CurrentTimer = 0;
    }
}
