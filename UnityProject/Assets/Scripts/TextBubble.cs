using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TextBubble : MonoBehaviour
{
    public static List<int> ItemsToShow { get; private set; }
    public static TextBubble Instance { get; private set; }
    public float TimeMsgAppeared = 0.0f;
    public int CurrentMsgId = 0;
    public GameObject textB;
    public TMP_Text messageText;
    public TextAsset textBText;
    public string[] dataLines;
    bool IsActive = true;
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
            //Перенёс из MainMenu.cs
            ItemsToShow = new List<int>();
            AddItemsToShow(new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7 }));
            dataLines = textBText.text.Split('\n');
            InvokeRepeating("ShowTextBubble", 0, 5.0f);
        }
    }
    #endregion
    //Это всё можно было бы вызывать вне класса, например TextBubble.Instance.AddRange(ItemsToShow)
    // И по сути обойтись без функций, но у меня стоит private set в поле. А всё делать публичным... ну такое
    public void AddItemsToShow(List<int> itemsToShow)
    {
        ItemsToShow.AddRange(itemsToShow);
    }
    public void AddItemsToShow(int itemsToShow)
    {
        ItemsToShow.Add(itemsToShow);
    }
    public void DeleteItemToShow(int ItemToDelete) 
    {
        ItemsToShow.Remove(ItemToDelete);
    }

    string GetInfo()
    {
        if (ItemsToShow.Count != 0)
        {
            var item = ItemsToShow[CurrentMsgId].ToString();
            for (int i = 0; i < dataLines.Length; i++)
            {
                var number = dataLines[i].Split(",")[0];
                var found = dataLines[i].IndexOf(",");
                var data = dataLines[i].Substring(found + 1);
                if (number == item)
                {
                    return data;
                }
            }
            return null;
        }
        return null;
    }

    void ShowTextBubble()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "EndingScene")
        {
            ItemsToShow = new List<int> { };
        }
        if (IsActive == false){
            CurrentMsgId = CurrentMsgId + 1;
            if (CurrentMsgId >= ItemsToShow.Count)
                CurrentMsgId = 0;
            var data = GetInfo();
            if (data != null)
            {
                messageText.SetText(data.ToString());
            }
        }
        IsActive = !IsActive;
        textB.SetActive(IsActive);
        if (ItemsToShow.Count == 0)
        {
            textB.SetActive(false);
        }
    }
    void HideTextBubble() //Попробовать вызывать при запуске мини-игры, после завершения снова вызывать InvokeRepeating,
                          //возможно из класса самой мини-игры через TextBubble.Instance
    {
        CancelInvoke("ShowTextBubble");// Как-то так
        textB.SetActive(false); // должно быть
    }
}



