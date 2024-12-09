using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    
    public static Timer Instance { get; private set; }
    public GameObject TimerText;

    private static int MiniGameTimer;
    private static int LocationTimer;
    private static bool IsMiniGamePlaying;
    private static bool IsPaused;
    
    #region Singleton паттерн
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            bool EasyMode = Convert.ToBoolean(PlayerPrefs.GetInt("EasyMode", 1));
            LocationTimer = EasyMode ? 120 : 60;
            IsPaused = false;
            IsMiniGamePlaying = false;
            StartCoroutine(DoTimer());
        }
    }
    #endregion
    public IEnumerator DoTimer()
    {
        while (LocationTimer >= 0)
        {
            while (IsPaused)
            {
                yield return null; // Ждем до следующего кадра
            }
            if (IsMiniGamePlaying) //Запущена мини-игра - работает таймер мини-игры
            {
                UpdateVisuals(MiniGameTimer);
                yield return new WaitForSeconds(1);
                MiniGameTimer--;
            }
            else // Иначе работает таймер локаций
            {
                UpdateVisuals(LocationTimer);
                yield return new WaitForSeconds(1);
                LocationTimer--;
            }
        }
        Ending.Instance.SetEndingType(false);
        SceneManager.LoadScene("EndingScene");
        Destroy(GameObject.Find("UICanvas"));
    }
    public void TogglePause() 
    {
        IsPaused = !IsPaused;
    }
    private void UpdateVisuals(int TimerType) 
    {
        if (TimerType >= 10)
        {
            string formattedTime = string.Format("{0:D2}:{1:D2}", TimerType/60, TimerType%60);
            TimerText.GetComponent<TextMeshProUGUI>().text = formattedTime;
        }
        else if (TimerType < 10)
        {
            string formattedTime = string.Format("{0:D2}:{1:D2}", TimerType/60, TimerType%60);
            TimerText.GetComponent<TextMeshProUGUI>().text = formattedTime;
            TimerText.GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        //if (TimerType == 0)
        //{
        //    Ending.Instance.ShowEnding(false);
        //    Destroy(GameObject.Find("UICanvas"));
        //    SceneManager.LoadScene("EndingScene");
        //}
    }
}
