using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static List<Item> SelectedItems { get; private set; }
    public static Inventory Instance { get; private set; }

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
    public void AddItem(Item ItemObject)
    {
        SelectedItems.Add(ItemObject);
        //TextBubble.Instance.DeleteItemToShow(ItemObject.ItemID);

        //Визуала пока нет, только список SelectedItems

        //Идея// Добавить функцию передачи (копирования?) предмета в окно инвентаря
        // установить дочерним элементом соответствующего объекта Cell в интерфейсе.
        // Либо Instantiate префабы предметов (Решение выше. И надо создать префабы предметов)
        // Либо предметы в инвентаре имеют статичное положение соответствующее айдишнику предмета
        // (Это более простая реализация и я думаю более подходящая, только как скопировать предмет? Хотя бы картинку)
    }
    public void NewInventory() // Обнуляем статическую переменную
    {
        SelectedItems = new List<Item>();
    }
    public List<int> GetItems() // Получаем ID предметов из инвентаря
    {
        List<int> ItemsID = new List<int>();
        foreach (var item in SelectedItems) 
        {
            ItemsID.Add(item.ItemID);
        }
        return ItemsID;
    }
}
