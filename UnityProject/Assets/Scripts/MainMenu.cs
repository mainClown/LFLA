using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame() 
    {
        TextBubble.Instance.NewItemsToShow();
        TextBubble.Instance.AddItemsToShow(new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7 }));
        SceneManager.LoadScene("BedroomScene");
    }

    public void Exit() 
    {
        Application.Quit();
    }

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
