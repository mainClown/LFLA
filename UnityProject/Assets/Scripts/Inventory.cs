using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static List<Item> SelectedItems { get; private set; }
    public static List<Item> UsedItems { get; private set; }
    public static Inventory Instance { get; private set; }

    private Transform[] InventoryUIList;
    public GameObject InventoryUI;
    public GameObject CellContainerObject;
    public GameObject CanvasUI;
    public GameObject ItemCounter;

    private int ItemCount;

    #region Singleton �������
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
            UsedItems = new List<Item>();
            ItemCount = 0;
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
    public void UseItem(Item ItemObject)
    {
        UsedItems.Add(ItemObject);
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
                if (ItemObject.InventorySprite != null)
                {
                    icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    icon.GetComponent<Image>().sprite = ItemObject.InventorySprite;
                    ItemCount += 1;
                }
                ItemCounter.GetComponent<TextMeshProUGUI>().text = ItemCount.ToString()+"/51";
                break;
            }
        }
    }
    public List<int> GetSelectedItemsID() // �������� ID ��������� �� ������
    {
        List<int> ItemsID = new List<int>();
        foreach (var item in SelectedItems) 
        {
            ItemsID.Add(item.ItemId);
        }
        return ItemsID;
    }
    //����� ���������, �� ��������, ��� ������ �� ��������\/\/\/
    public List<int> GetUsedItemsID() // �������� ID ��������� �� ������
    {
        List<int> ItemsID = new List<int>();
        foreach (var item in UsedItems)
        {
            ItemsID.Add(item.ItemId);
        }
        return ItemsID;
    }
    // Временно для тестов
    public void SetInstanceForTests()
    {
        Instance = this;
    }

}
