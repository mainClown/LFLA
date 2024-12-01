using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static List<Item> SelectedItems { get; private set; }
    public static Inventory Instance { get; private set; }

    private Transform[] InventoryUIList;
    public GameObject InventoryUI;
    public GameObject CellContainerObject;
    public GameObject CanvasUI;

    #region Singleton паттерн
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Destroy(CanvasUI);
        }
        else
        {
            Instance = this;
            SelectedItems = new List<Item>();
            DontDestroyOnLoad(target: this);
        }
    }
    #endregion
    public void AddItem(Item ItemObject)
    {
        SelectedItems.Add(ItemObject);
        UpdateVisual(ItemObject);
        //TextBubble.Instance.DeleteItemToShow(ItemObject.ItemID);
    }
    public void ShowInventory()
    {
        InventoryUI.SetActive(true);
    }
    public void CloseInventory()
    {
        InventoryUI.SetActive(false);
    }
    private void UpdateVisual(Item ItemObject)
    {
        InventoryUIList = CellContainerObject.GetComponentsInChildren<Transform>();

        foreach (Transform childObject in InventoryUIList)
        {
            Transform cell = childObject;
            Image icon = cell.GetComponent<Image>();

            if (icon.sprite == null)
            {
                icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                icon.GetComponent<Image>().sprite = ItemObject.GetComponent<Image>().sprite;
                break;
            }
        }
    }
    public void NewInventory() // Обнуляем статическую переменную
    {
        SelectedItems = new List<Item>();
    }
    public List<int> GetItemsID() // Получаем ID предметов из инвентаря
    {
        List<int> ItemsID = new List<int>();
        foreach (var item in SelectedItems) 
        {
            ItemsID.Add(item.ItemID);
        }
        return ItemsID;
    }
}
