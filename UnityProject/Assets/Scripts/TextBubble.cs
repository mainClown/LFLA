using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextBubble : MonoBehaviour
{
    //Много всего надо сделать, лучше всего использовать Корутины, так как показ пузыря у нас периодический.
    public static List<int> ItemsToShow { get; private set; }
    public static TextBubble Instance { get; private set; }

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
            DontDestroyOnLoad(target: this);
        }
    }
    #endregion
    public void ContinueGame()
    {
        //Не помню чё я тут хотел сделать
    }
    public void NewItemsToShow() // Обнуляем статические переменные
    {
        ItemsToShow = new List<int>();
    }
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
}
