using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextBubble : MonoBehaviour
{
    public static List<Item> ItemsToShow { get; private set; }
    public static TextBubble Instance { get; private set; }
    public void StartGame()
    {
        ItemsToShow = new List<Item>();
    }
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
    public void ContinueGame() 
    {

    }

    public void AddItemsToShow(List<Item> itemsToShow)
    {
        ItemsToShow.AddRange(itemsToShow);
    }
    public void DeleteItemsToShow(Item ItemToDelete) 
    {
        ItemsToShow.Remove(ItemToDelete);
    }
}
