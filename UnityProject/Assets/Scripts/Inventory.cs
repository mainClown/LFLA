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

    #region Singleton �������
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

        //������� ���� ���, ������ ������ SelectedItems

        //����// �������� ������� �������� (�����������?) �������� � ���� ���������
        // ���������� �������� ��������� ���������������� ������� Cell � ����������.
        // ���� Instantiate ������� ��������� (������� ����. � ���� ������� ������� ���������)
        // ���� �������� � ��������� ����� ��������� ��������� ��������������� ��������� ��������
        // (��� ����� ������� ���������� � � ����� ����� ����������, ������ ��� ����������� �������? ���� �� ��������)
    }
    public void NewInventory() // �������� ����������� ����������
    {
        SelectedItems = new List<Item>();
    }
    public List<int> GetItems() // �������� ID ��������� �� ���������
    {
        List<int> ItemsID = new List<int>();
        foreach (var item in SelectedItems) 
        {
            ItemsID.Add(item.ItemID);
        }
        return ItemsID;
    }
}
