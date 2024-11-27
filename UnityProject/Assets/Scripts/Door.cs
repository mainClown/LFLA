using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //NotNecessaryItems ����� ������ ��� � ����� ���
    public List<int> NecessaryItems; // ��������� ������������ ���������
    private static List<int> InventoryItems;
    //� ������� � ��� � ���, ��� ��� ������� �� ���������, ������
    public bool CheckTransition()
    {
        InventoryItems = Inventory.Instance.GetItems();
        return InventoryItems.Count >= NecessaryItems.Count && NecessaryItems.All(InventoryItems.Contains);
    }
    public void OpenLocation(string LocationName)
    {
        if (CheckTransition())
        {
            SceneManager.LoadScene(LocationName);
        }
        else
        {
            //TextBubble.Instance.ShowAndHideTextBubble() 
            //����-��� �� ���� �������������
        }
    }
}
