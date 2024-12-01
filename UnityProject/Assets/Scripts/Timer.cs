using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // ��� � ���������
    // ����� �������
    // P.S. ������ �� ����� �������, UI ��� ���� �������
    public float MaxTimer = 60f;
    public float CurrentTimer;
    // ����������� �������� �������, �� ������������ �������
    public void StartTimer() 
    {
        CurrentTimer = MaxTimer;
        while (CurrentTimer > 0)
        {
            CurrentTimer -= Time.deltaTime; // Time.deltaTime - ����� ����� ������ ������ � ��������
        }
        CurrentTimer = 0;
    }
}
