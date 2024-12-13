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

    private int MiniGameTimer;
    private static int LocationTimer;
    private static bool IsMiniGamePlaying;
    private static bool IsPaused;
    private static bool EasyMode;
    public event Action OnMiniGameEnd;
    
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
            EasyMode = Convert.ToBoolean(PlayerPrefs.GetInt("EasyMode", 1));
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
                yield return null;
            }
            if (IsMiniGamePlaying)
            {
                while (MiniGameTimer >= 0)
                {
                    while (IsPaused)
                    {
                        yield return null;
                    }
                    UpdateVisuals(MiniGameTimer);
                    yield return new WaitForSeconds(1);
                    MiniGameTimer--;
                }
                OnMiniGameEnd?.Invoke();
            }
            else
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
            TimerText.GetComponent<TextMeshProUGUI>().color = Color.black;
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
    public void SetMiniGameTimer(int MiniGameTimeSeconds) 
    {
        MiniGameTimer = MiniGameTimeSeconds /( EasyMode ? 1 : 2); 
        IsMiniGamePlaying = true;
    }
    public void ResetMiniGameTimer()
    {
        MiniGameTimer = 0;
        IsMiniGamePlaying = false;
    }
}
