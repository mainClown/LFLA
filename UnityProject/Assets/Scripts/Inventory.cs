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

    public void StartNewInventory()
    {
        SelectedItems = new List<Item>();
    }
    //Singleton �������, �� ������ � �������� ������.
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
    public void AddItem(Item ItemObject)
    {
        SelectedItems.Add(ItemObject);
        //TextBubble.Instance.DeleteItemsToShow(ItemObject);


        //����// �������� ������� �������� �������� � ���� ���������
        // ���������� �������� ��������� ���������������� ������� Cell � ����������.
    }
    public List<int> GetItems() 
    {
        List<int> ItemsID = new List<int>();
        foreach (var item in SelectedItems) 
        {
            ItemsID.Add(item.ItemID);
        }
        return ItemsID;
    }
}
