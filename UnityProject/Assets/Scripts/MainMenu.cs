using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif

public class MainMenu : MonoBehaviour
{
    public void StartNewGame() 
    {
        //TextBubble ������ ������� � ��� Awake
        SceneManager.LoadScene("BedroomScene");
    }

    public void Exit() 
    {
        Application.Quit();
    }

    // Для теста В4а - имитация ошибок
    // public void Exit()
    // {
    //     try
    //     {
    //         // Имитация ошибки
    //         if (HasActiveProcesses())
    //         {
    //             throw new Exception("Cannot quit: active processes detected");
    //         }

    //         Application.Quit();
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError($"Error during quit: {ex.Message}");
    //     }
    // }

    // private bool HasActiveProcesses()
    // {
    //     // Имитация проверки на активные процессы
    //     return true; // Вернуть true для имитации ошибки
    // }
    

    /*���� ���� ��� � � Door.cs ����� �������� �� ������� ��� �� �������, ������������ ����������� ����� � ��������
    public Button Start_Button;
    private IEnumerator StartNewGame()
    {
        TextBubble.Instance.NewItemsToShow();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BedroomScene");

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                // ��� 3 �������
                yield return new WaitForSeconds(1);

                // ������������� �� ����������� �����
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public void StartButton()
    {
        StartCoroutine(StartNewGame());
        Start_Button.interactable = false;
    }*/
}
