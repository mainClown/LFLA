using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

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
    //Сори, Даша, если что-то поломал
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
            DontDestroyOnLoad(target: this);
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
        if (CurrentMsgId >= dataLines.Length)
            CurrentMsgId = 0;
        var item = ItemsToShow[CurrentMsgId].ToString();
        for (int i = 0; i < dataLines.Length; i++)
        {
            var data = dataLines[CurrentMsgId].Split(',');
            if (data[0]== item)
            {
                return data[1];
            }
        }
        return null;
    }

    void ShowTextBubble()
    {
        if (IsActive == false){
            CurrentMsgId = CurrentMsgId + 1;
            var data = GetInfo();
            Debug.Log(data);
            messageText.SetText(data.ToString());
        }
        IsActive = !IsActive;
        textB.SetActive(IsActive);
    }
    void HideTextBubble()
    {
        textB.SetActive(false);
    }
}



