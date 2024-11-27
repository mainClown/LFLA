using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TextBubble : MonoBehaviour
{
    //����� ����� ���� �������, ����� ����� ������������ ��������, ��� ��� ����� ������ � ��� �������������.
    public static List<int> ItemsToShow { get; private set; }
    public static TextBubble Instance { get; private set; }

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
    public void ContinueGame()
    {
        //�� ����� �� � ��� ����� �������
    }
    public void NewItemsToShow() // �������� ����������� ����������
    {
        ItemsToShow = new List<int>();
    }
    //��� �� ����� ���� �� �������� ��� ������, �������� TextBubble.Instance.AddRange(ItemsToShow)
    // � �� ���� �������� ��� �������, �� � ���� ����� private set � ����. � �� ������ ���������... �� �����
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
